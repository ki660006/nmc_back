Imports System.Net
Imports Microsoft.Win32

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports LISAPP.APP_DB
Imports LOGIN

Public Class MAIN
    Inherits System.Windows.Forms.Form

    Private msFile As String = "File : MAIN.vb, Class : MAIN" + vbTab
    Private msTitle As String = My.Application.Info.ProductName '+ " (" + My.Application.Info.Title + ")"

    ' 자동로그인 OCS에서 자동연결
    Private mbAutoLogin As Boolean = False

    Private Const msXMLDir As String = "\XML"
    Private msAutoLogout As String = Application.StartupPath + msXMLDir & "\AUTOLOGOUT.XML"
    Private msNLS_LANG As String = Application.StartupPath + msXMLDir & "\NLS_LANG.XML"

    '> OCS 연결방식 : 매번 새 프로세스 호출하는 경우
    Private msPathArgs As String = Application.StartupPath + "\ArgsInfo.txt"
    Private msPathArgsDetail As String = Application.StartupPath + "\ArgsDetail.txt"


    Private m_tooltip As ToolTip

    '    Private m_al_picShortBtn As ArrayList
    Private m_al_mnuAll As ArrayList
    Private m_al_Args As New ArrayList

    Private mbForceUpdate As Boolean = False
    Friend WithEvents mnuO As System.Windows.Forms.MenuItem
    Friend WithEvents mnuO_input As System.Windows.Forms.MenuItem
    Friend WithEvents mnuO_srch As System.Windows.Forms.MenuItem
    Friend WithEvents mnuM_abn As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_abn2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_cross_rst As System.Windows.Forms.MenuItem
    Friend WithEvents mnuM_wl_exe As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJ_take2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuM_wl As System.Windows.Forms.MenuItem
    Friend WithEvents mnuM_hr0 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_tk_list As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_hyph01 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR_hyph02 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR_ks As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_coll_st As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_ReTest As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mnuM_rstsheet As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_FnDataCmt As System.Windows.Forms.MenuItem
    Friend WithEvents mnuO_hr As System.Windows.Forms.MenuItem
    Friend WithEvents mnuO_ord As System.Windows.Forms.MenuItem
    Friend WithEvents mnuO_base As System.Windows.Forms.MenuItem
    Friend WithEvents mnuC_cust As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_aborh_rst As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR_icu As System.Windows.Forms.MenuItem
    Friend WithEvents mnuO_custlist As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJ_tk_icu As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR_chg_regno As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_tat_tcls As System.Windows.Forms.MenuItem
    Friend WithEvents sbpUser As System.Windows.Forms.StatusBarPanel
    Friend WithEvents btnHotList As CButtonLib.CButton
    Friend WithEvents btnLogout As CButtonLib.CButton
    Friend WithEvents mnu_hr1 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuExit As System.Windows.Forms.MenuItem
    Friend WithEvents btnHotm03 As System.Windows.Forms.Button
    Friend WithEvents btnHotm02 As System.Windows.Forms.Button
    Friend WithEvents btnHotm01 As System.Windows.Forms.Button
    Friend WithEvents btnHotm00 As System.Windows.Forms.Button
    Friend WithEvents btnHotm06 As System.Windows.Forms.Button
    Friend WithEvents btnHotm05 As System.Windows.Forms.Button
    Friend WithEvents btnHotm04 As System.Windows.Forms.Button
    Friend WithEvents btnHotm09 As System.Windows.Forms.Button
    Friend WithEvents btnHotm08 As System.Windows.Forms.Button
    Friend WithEvents btnHotm07 As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnTabClose As System.Windows.Forms.Button
    Friend WithEvents tbcMenu As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents ImageList As System.Windows.Forms.ImageList
    Friend WithEvents mnuB94 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJ_ExLab As System.Windows.Forms.MenuItem
    Friend WithEvents mnuC_ward_batch As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_outabn_dept As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_XMatch_cnt As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_UnfitSpc As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_hr1 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_hr2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_hr0 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuM_hr1 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuF_test As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR_poct As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJ_pass As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJ_PassTake As System.Windows.Forms.MenuItem
    Friend WithEvents mnuC_pis_out As System.Windows.Forms.MenuItem
    Friend WithEvents mnuC_pis_ward As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_st_dr As System.Windows.Forms.MenuItem
    Friend WithEvents btnLock As CButtonLib.CButton
    Friend WithEvents mnuJ_hr As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJ_wl As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_hr3 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_op As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_NotColl As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_aborh_prt As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_st_dept As System.Windows.Forms.MenuItem
    Friend WithEvents sbpAutoLogon As System.Windows.Forms.StatusBarPanel
    Friend WithEvents btnAutoLogon As System.Windows.Forms.Button
    Friend WithEvents mnuSet_EmrPrt As System.Windows.Forms.MenuItem
    Friend WithEvents mnu_hr2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAS As System.Windows.Forms.MenuItem
    Friend WithEvents mnu_hr3 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_IO As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_Hos As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_st_spc As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_exec As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_Hos2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MnuRef As System.Windows.Forms.MenuItem
    Friend WithEvents mnuO_test As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR_exlab_scl As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_rstcnt As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR_exlab_sml As System.Windows.Forms.MenuItem
    Friend WithEvents MnuTest2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuQ03 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuO_Ncov As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR_exlab_gcl As System.Windows.Forms.MenuItem

    Private LoginPopWin As New LoginPopWin

    Private Sub sbKillPreProc_test()
        Dim a_proc As Process() = Diagnostics.Process.GetProcesses()

        If a_proc.Length > 0 Then
            For ix As Integer = 0 To a_proc.Length - 1
                If a_proc(ix).ProcessName = "EMR_TESTo" Then
                    a_proc(ix).Kill()
                End If
            Next
        End If
    End Sub

    Private Sub sbChange_srv()

#If DEBUG Then
        Return
#End If

        Dim stuCStr As COMMON.CommDb.STU_CONNSTR
        stuCStr = (New COMMON.CommDb.Info).GetConnStr

        With stuCStr
            .USEDP = "2"
            .PROVIDER = stuCStr.PROVIDER       'SQLOLEDB, MSDAORA
            .CATEGORY = stuCStr.CATEGORY

            .USERID = "lisif"
            .PASSWORD = "lisif"

            '-- 운영서버(1)
            .DATASOURCE = "(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST = 10.95.21.144)(PORT=1521))(ADDRESS=(PROTOCOL=TCP)(HOST=10.95.21.143)(PORT=1521))(LOAD_BALANCE=NO)(CONNECT_DATA=(SERVER= DEDICATED)(SERVICE_NAME=EMRDB)(FAILOVER_MODE=(TYPE=SELECT)(METHOD=BASIC)(RETRIES=180)(DELAY=5))))"
            .DESCRIPTION = "PROD_EMRDB1"
            If Not MdiMain.Frm Is Nothing Then
                MdiMain.Frm.Text.Replace("NMC", "PROD_EMRDB1").Replace("EMRDB_DEV", "PROD_EMRDB1").Replace("MIGDB", "PROD_EMRDB1").Replace("PROD_EMRDB2", "PROD_EMRDB1")
            End If
        End With

        If (New COMMON.CommDb.Info).SetConnStr(stuCStr) = True Then
            Try
                FileCopy(Application.StartupPath + "\XML\DBSERVER.XML", Application.StartupPath + "\DEP\XML\DBSERVER.XML")
            Catch ex As Exception

            End Try
        End If

    End Sub


    Private Sub sbSetting_EmrPrint()
        Dim sFn As String = "Handles btnPrintSet.Click"

        Dim objFrm As New POPUPPRT.FGPOUP_PRT("EMRIMG")

        Try
            objFrm.ShowDialog()
            objFrm.Dispose()
            objFrm = Nothing

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Public Sub sbTabPageAdd(ByVal r_frm As Form)
        Dim sFn As String = "Private Sub sbTabPageAdd(Form)"

        Try
            Dim iExist As Integer = -1

            For ix As Integer = 0 To Me.tbcMenu.TabCount - 1
                If Me.tbcMenu.TabPages(ix).Text = r_frm.Text.Substring(r_frm.Text.IndexOf("ː") + 1) Then
                    iExist = ix

                    Exit For
                End If
            Next

            If iExist < 0 Then
                Me.tbcMenu.TabPages.Add(r_frm.Name)
                Me.tbcMenu.TabPages(tbcMenu.TabCount - 1).Text = r_frm.Text.Substring(r_frm.Text.IndexOf("ː") + 1)

                iExist = tbcMenu.TabCount - 1
            End If

            Me.tbcMenu.SelectedIndex = iExist

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, "MidPicBox - " + sFn)

        End Try
    End Sub

    Private Sub sbSetting_SystemTime()

        Try
            Dim dtSvrDateTime As Date = (New ServerDateTime).GetDateTimeWithNewCn

            Microsoft.VisualBasic.DateAndTime.DateString = Format(dtSvrDateTime, "yyyy-MM-dd").ToString
            Microsoft.VisualBasic.DateAndTime.TimeString = Format(dtSvrDateTime, "HH:mm:ss").ToString

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sbKillPreProc_old()
        Dim a_proc As Process() = Diagnostics.Process.GetProcesses()

        If a_proc.Length > 0 Then
            For ix As Integer = 0 To a_proc.Length - 1
                If a_proc(ix).ProcessName = "ACK@LIS" Then
                    a_proc(ix).Kill()
                End If
            Next
        End If
    End Sub


#Region " Windows Form 디자이너에서 생성한 코드 "
    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbFormInitialize()
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
    Friend WithEvents stbMain As System.Windows.Forms.StatusBar
    Friend WithEvents sbpTitleMsg As System.Windows.Forms.StatusBarPanel
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents sbpTitleDt As System.Windows.Forms.StatusBarPanel
    Friend WithEvents mnuC_lab As System.Windows.Forms.MenuItem
    Friend WithEvents mnuC_ward As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJ_take As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJ_cancel As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJ_bc_reprint As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem18 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuWindow As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCascade As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTileHorizontal As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTileVertical As System.Windows.Forms.MenuItem
    Friend WithEvents mnuC As System.Windows.Forms.MenuItem
    Friend WithEvents mnuJ As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR_sample As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR_labusr As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR_item As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_abn As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_tat As System.Windows.Forms.MenuItem
    Friend WithEvents sbpMsg As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbpDateTime As System.Windows.Forms.StatusBarPanel
    Friend WithEvents mnuM As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB00 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuF00 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuF01 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB015 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_tns_jubsu As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_bef_reg As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_bef_cancel As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_out As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_abn As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_abn_part As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_bld_history As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_tns_list As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_bld_io_st As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_bcno_rst As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_test_rst As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_keep As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_abn_st As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMediack As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAbout As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_wklist As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_rvo As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_rsheet As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_ordstate As System.Windows.Forms.MenuItem
    Friend WithEvents mmnuLIS As System.Windows.Forms.MainMenu
    Friend WithEvents pnlProgress As System.Windows.Forms.Panel
    Friend WithEvents pbrMain As System.Windows.Forms.ProgressBar
    Friend WithEvents tmrNowDateTime As System.Windows.Forms.Timer
    Friend WithEvents tmrAutoUpdate As System.Windows.Forms.Timer
    Friend WithEvents mnuB_abn_list As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_st As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_rvt As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB047 As System.Windows.Forms.MenuItem
    Friend WithEvents btnTray As System.Windows.Forms.Button
    Friend WithEvents niTray As System.Windows.Forms.NotifyIcon
    Friend WithEvents mnuS_stcoll As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_ordhistory As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_clist As System.Windows.Forms.MenuItem
    Friend WithEvents mnuM_sample As System.Windows.Forms.MenuItem
    Friend WithEvents mnuM_ng As System.Windows.Forms.MenuItem
    Friend WithEvents mnuM_item As System.Windows.Forms.MenuItem
    Friend WithEvents mnuM_gr As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_abn_rst As System.Windows.Forms.MenuItem
    Friend WithEvents mnuQ00 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR_hyph01 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuQ01 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuQ02 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR_exlab As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR_special As System.Windows.Forms.MenuItem
    Friend WithEvents lblVer As System.Windows.Forms.Label
    Friend WithEvents picLogo As System.Windows.Forms.PictureBox
    Friend WithEvents mnuB048 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB_abo_rst As System.Windows.Forms.MenuItem
    Friend WithEvents mnuB049 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuC_out As System.Windows.Forms.MenuItem
    Friend WithEvents mnuM_statistics As System.Windows.Forms.MenuItem
    Friend WithEvents mnuR_wl As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_sttat As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_rv_u As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_rv_a As System.Windows.Forms.MenuItem
    Friend WithEvents mnuM_ks As System.Windows.Forms.MenuItem
    Friend WithEvents mnuS_st_fn As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MAIN))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.stbMain = New System.Windows.Forms.StatusBar()
        Me.sbpTitleMsg = New System.Windows.Forms.StatusBarPanel()
        Me.sbpMsg = New System.Windows.Forms.StatusBarPanel()
        Me.sbpUser = New System.Windows.Forms.StatusBarPanel()
        Me.sbpTitleDt = New System.Windows.Forms.StatusBarPanel()
        Me.sbpDateTime = New System.Windows.Forms.StatusBarPanel()
        Me.sbpAutoLogon = New System.Windows.Forms.StatusBarPanel()
        Me.mmnuLIS = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuO = New System.Windows.Forms.MenuItem()
        Me.mnuO_input = New System.Windows.Forms.MenuItem()
        Me.mnuO_srch = New System.Windows.Forms.MenuItem()
        Me.mnuO_hr = New System.Windows.Forms.MenuItem()
        Me.mnuO_ord = New System.Windows.Forms.MenuItem()
        Me.mnuO_custlist = New System.Windows.Forms.MenuItem()
        Me.mnuO_base = New System.Windows.Forms.MenuItem()
        Me.mnuO_test = New System.Windows.Forms.MenuItem()
        Me.mnuO_Ncov = New System.Windows.Forms.MenuItem()
        Me.mnuC = New System.Windows.Forms.MenuItem()
        Me.mnuC_lab = New System.Windows.Forms.MenuItem()
        Me.mnuC_ward = New System.Windows.Forms.MenuItem()
        Me.mnuC_cust = New System.Windows.Forms.MenuItem()
        Me.mnuC_out = New System.Windows.Forms.MenuItem()
        Me.mnuC_ward_batch = New System.Windows.Forms.MenuItem()
        Me.mnuC_pis_out = New System.Windows.Forms.MenuItem()
        Me.mnuC_pis_ward = New System.Windows.Forms.MenuItem()
        Me.mnuJ = New System.Windows.Forms.MenuItem()
        Me.mnuJ_pass = New System.Windows.Forms.MenuItem()
        Me.mnuJ_PassTake = New System.Windows.Forms.MenuItem()
        Me.mnuJ_take = New System.Windows.Forms.MenuItem()
        Me.mnuJ_cancel = New System.Windows.Forms.MenuItem()
        Me.mnuJ_bc_reprint = New System.Windows.Forms.MenuItem()
        Me.mnuJ_take2 = New System.Windows.Forms.MenuItem()
        Me.mnuJ_tk_icu = New System.Windows.Forms.MenuItem()
        Me.mnuJ_hr = New System.Windows.Forms.MenuItem()
        Me.mnuJ_wl = New System.Windows.Forms.MenuItem()
        Me.mnuJ_ExLab = New System.Windows.Forms.MenuItem()
        Me.mnuR = New System.Windows.Forms.MenuItem()
        Me.mnuR_sample = New System.Windows.Forms.MenuItem()
        Me.mnuR_labusr = New System.Windows.Forms.MenuItem()
        Me.mnuR_item = New System.Windows.Forms.MenuItem()
        Me.mnuR_wl = New System.Windows.Forms.MenuItem()
        Me.mnuR_exlab = New System.Windows.Forms.MenuItem()
        Me.mnuR_exlab_scl = New System.Windows.Forms.MenuItem()
        Me.mnuR_hyph01 = New System.Windows.Forms.MenuItem()
        Me.mnuR_special = New System.Windows.Forms.MenuItem()
        Me.mnuR_icu = New System.Windows.Forms.MenuItem()
        Me.mnuR_poct = New System.Windows.Forms.MenuItem()
        Me.mnuR_hyph02 = New System.Windows.Forms.MenuItem()
        Me.mnuR_ks = New System.Windows.Forms.MenuItem()
        Me.mnuR_chg_regno = New System.Windows.Forms.MenuItem()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.mnuR_exlab_sml = New System.Windows.Forms.MenuItem()
        Me.mnuR_exlab_gcl = New System.Windows.Forms.MenuItem()
        Me.mnuS = New System.Windows.Forms.MenuItem()
        Me.mnuS_tk_list = New System.Windows.Forms.MenuItem()
        Me.mnuS_wklist = New System.Windows.Forms.MenuItem()
        Me.mnuS_rsheet = New System.Windows.Forms.MenuItem()
        Me.mnuS_abn = New System.Windows.Forms.MenuItem()
        Me.mnuS_abn2 = New System.Windows.Forms.MenuItem()
        Me.mnuS_ordstate = New System.Windows.Forms.MenuItem()
        Me.mnuS_ordhistory = New System.Windows.Forms.MenuItem()
        Me.mnuS_NotColl = New System.Windows.Forms.MenuItem()
        Me.mnuS_clist = New System.Windows.Forms.MenuItem()
        Me.mnuS_UnfitSpc = New System.Windows.Forms.MenuItem()
        Me.mnuS_abn_rst = New System.Windows.Forms.MenuItem()
        Me.mnuS_ReTest = New System.Windows.Forms.MenuItem()
        Me.mnuS_FnDataCmt = New System.Windows.Forms.MenuItem()
        Me.mnuS_tat = New System.Windows.Forms.MenuItem()
        Me.mnuS_tat_tcls = New System.Windows.Forms.MenuItem()
        Me.mnuS_stcoll = New System.Windows.Forms.MenuItem()
        Me.mnuS_st = New System.Windows.Forms.MenuItem()
        Me.mnuS_st_dr = New System.Windows.Forms.MenuItem()
        Me.mnuS_st_dept = New System.Windows.Forms.MenuItem()
        Me.mnuS_sttat = New System.Windows.Forms.MenuItem()
        Me.mnuS_coll_st = New System.Windows.Forms.MenuItem()
        Me.mnuS_st_fn = New System.Windows.Forms.MenuItem()
        Me.mnuS_Hos = New System.Windows.Forms.MenuItem()
        Me.mnuS_hyph01 = New System.Windows.Forms.MenuItem()
        Me.mnuS_rvo = New System.Windows.Forms.MenuItem()
        Me.mnuS_rvt = New System.Windows.Forms.MenuItem()
        Me.mnuS_rv_a = New System.Windows.Forms.MenuItem()
        Me.mnuS_rv_u = New System.Windows.Forms.MenuItem()
        Me.mnuS_st_spc = New System.Windows.Forms.MenuItem()
        Me.mnuS_exec = New System.Windows.Forms.MenuItem()
        Me.mnuS_Hos2 = New System.Windows.Forms.MenuItem()
        Me.mnuS_rstcnt = New System.Windows.Forms.MenuItem()
        Me.mnuB00 = New System.Windows.Forms.MenuItem()
        Me.mnuB015 = New System.Windows.Forms.MenuItem()
        Me.mnuB_hr0 = New System.Windows.Forms.MenuItem()
        Me.mnuB_tns_jubsu = New System.Windows.Forms.MenuItem()
        Me.mnuB_bef_reg = New System.Windows.Forms.MenuItem()
        Me.mnuB_out = New System.Windows.Forms.MenuItem()
        Me.mnuB_abn = New System.Windows.Forms.MenuItem()
        Me.mnuB_abn_part = New System.Windows.Forms.MenuItem()
        Me.mnuB_keep = New System.Windows.Forms.MenuItem()
        Me.mnuB_bef_cancel = New System.Windows.Forms.MenuItem()
        Me.mnuB_hr1 = New System.Windows.Forms.MenuItem()
        Me.mnuB_bld_history = New System.Windows.Forms.MenuItem()
        Me.mnuB_tns_list = New System.Windows.Forms.MenuItem()
        Me.mnuB047 = New System.Windows.Forms.MenuItem()
        Me.mnuB_abn_list = New System.Windows.Forms.MenuItem()
        Me.mnuB_bld_io_st = New System.Windows.Forms.MenuItem()
        Me.mnuB049 = New System.Windows.Forms.MenuItem()
        Me.mnuB_abn_st = New System.Windows.Forms.MenuItem()
        Me.mnuB94 = New System.Windows.Forms.MenuItem()
        Me.mnuB_outabn_dept = New System.Windows.Forms.MenuItem()
        Me.mnuB_XMatch_cnt = New System.Windows.Forms.MenuItem()
        Me.mnuB048 = New System.Windows.Forms.MenuItem()
        Me.mnuB_hr2 = New System.Windows.Forms.MenuItem()
        Me.mnuB_bcno_rst = New System.Windows.Forms.MenuItem()
        Me.mnuB_test_rst = New System.Windows.Forms.MenuItem()
        Me.mnuB_abo_rst = New System.Windows.Forms.MenuItem()
        Me.mnuB_aborh_rst = New System.Windows.Forms.MenuItem()
        Me.mnuB_cross_rst = New System.Windows.Forms.MenuItem()
        Me.mnuB_aborh_prt = New System.Windows.Forms.MenuItem()
        Me.mnuB_hr3 = New System.Windows.Forms.MenuItem()
        Me.mnuB_op = New System.Windows.Forms.MenuItem()
        Me.mnuB_IO = New System.Windows.Forms.MenuItem()
        Me.mnuM = New System.Windows.Forms.MenuItem()
        Me.mnuM_sample = New System.Windows.Forms.MenuItem()
        Me.mnuM_item = New System.Windows.Forms.MenuItem()
        Me.mnuM_ng = New System.Windows.Forms.MenuItem()
        Me.mnuM_hr0 = New System.Windows.Forms.MenuItem()
        Me.mnuM_wl = New System.Windows.Forms.MenuItem()
        Me.mnuM_wl_exe = New System.Windows.Forms.MenuItem()
        Me.mnuM_gr = New System.Windows.Forms.MenuItem()
        Me.mnuM_abn = New System.Windows.Forms.MenuItem()
        Me.mnuM_rstsheet = New System.Windows.Forms.MenuItem()
        Me.mnuM_statistics = New System.Windows.Forms.MenuItem()
        Me.mnuM_hr1 = New System.Windows.Forms.MenuItem()
        Me.mnuM_ks = New System.Windows.Forms.MenuItem()
        Me.mnuQ00 = New System.Windows.Forms.MenuItem()
        Me.mnuQ01 = New System.Windows.Forms.MenuItem()
        Me.mnuQ02 = New System.Windows.Forms.MenuItem()
        Me.mnuQ03 = New System.Windows.Forms.MenuItem()
        Me.mnuF00 = New System.Windows.Forms.MenuItem()
        Me.mnuF01 = New System.Windows.Forms.MenuItem()
        Me.mnuF_test = New System.Windows.Forms.MenuItem()
        Me.MnuRef = New System.Windows.Forms.MenuItem()
        Me.mnuWindow = New System.Windows.Forms.MenuItem()
        Me.mnuCascade = New System.Windows.Forms.MenuItem()
        Me.mnuTileHorizontal = New System.Windows.Forms.MenuItem()
        Me.mnuTileVertical = New System.Windows.Forms.MenuItem()
        Me.mnuMediack = New System.Windows.Forms.MenuItem()
        Me.mnuAbout = New System.Windows.Forms.MenuItem()
        Me.mnuHelp = New System.Windows.Forms.MenuItem()
        Me.mnu_hr1 = New System.Windows.Forms.MenuItem()
        Me.mnuSet_EmrPrt = New System.Windows.Forms.MenuItem()
        Me.mnu_hr2 = New System.Windows.Forms.MenuItem()
        Me.mnuAS = New System.Windows.Forms.MenuItem()
        Me.mnu_hr3 = New System.Windows.Forms.MenuItem()
        Me.mnuExit = New System.Windows.Forms.MenuItem()
        Me.MnuTest2 = New System.Windows.Forms.MenuItem()
        Me.MenuItem18 = New System.Windows.Forms.MenuItem()
        Me.pnlProgress = New System.Windows.Forms.Panel()
        Me.pbrMain = New System.Windows.Forms.ProgressBar()
        Me.tmrNowDateTime = New System.Windows.Forms.Timer(Me.components)
        Me.tmrAutoUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.niTray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnTabClose = New System.Windows.Forms.Button()
        Me.tbcMenu = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.btnAutoLogon = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.btnLock = New CButtonLib.CButton()
        Me.btnHotm09 = New System.Windows.Forms.Button()
        Me.btnHotm08 = New System.Windows.Forms.Button()
        Me.btnHotm07 = New System.Windows.Forms.Button()
        Me.btnHotm06 = New System.Windows.Forms.Button()
        Me.btnHotm05 = New System.Windows.Forms.Button()
        Me.btnHotm04 = New System.Windows.Forms.Button()
        Me.btnHotm03 = New System.Windows.Forms.Button()
        Me.btnHotm02 = New System.Windows.Forms.Button()
        Me.btnHotm01 = New System.Windows.Forms.Button()
        Me.btnHotm00 = New System.Windows.Forms.Button()
        Me.btnHotList = New CButtonLib.CButton()
        Me.btnTray = New System.Windows.Forms.Button()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.lblVer = New System.Windows.Forms.Label()
        Me.btnLogout = New CButtonLib.CButton()
        CType(Me.sbpTitleMsg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbpMsg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbpUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbpTitleDt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbpDateTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbpAutoLogon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlProgress.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.tbcMenu.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'stbMain
        '
        Me.stbMain.Location = New System.Drawing.Point(0, 671)
        Me.stbMain.Name = "stbMain"
        Me.stbMain.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbpTitleMsg, Me.sbpMsg, Me.sbpUser, Me.sbpTitleDt, Me.sbpDateTime, Me.sbpAutoLogon})
        Me.stbMain.ShowPanels = True
        Me.stbMain.Size = New System.Drawing.Size(1272, 25)
        Me.stbMain.TabIndex = 1
        '
        'sbpTitleMsg
        '
        Me.sbpTitleMsg.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.sbpTitleMsg.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.sbpTitleMsg.Name = "sbpTitleMsg"
        Me.sbpTitleMsg.Text = "MESSAGE:"
        Me.sbpTitleMsg.Width = 80
        '
        'sbpMsg
        '
        Me.sbpMsg.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.sbpMsg.Name = "sbpMsg"
        Me.sbpMsg.Width = 786
        '
        'sbpUser
        '
        Me.sbpUser.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.sbpUser.Name = "sbpUser"
        Me.sbpUser.Text = "관리자"
        Me.sbpUser.Width = 80
        '
        'sbpTitleDt
        '
        Me.sbpTitleDt.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.sbpTitleDt.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.sbpTitleDt.Name = "sbpTitleDt"
        Me.sbpTitleDt.Text = "일시:"
        Me.sbpTitleDt.Width = 39
        '
        'sbpDateTime
        '
        Me.sbpDateTime.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.sbpDateTime.Name = "sbpDateTime"
        Me.sbpDateTime.Text = "2003-12-12(목) 23:00:00"
        Me.sbpDateTime.Width = 150
        '
        'sbpAutoLogon
        '
        Me.sbpAutoLogon.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.sbpAutoLogon.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
        Me.sbpAutoLogon.MinWidth = 20
        Me.sbpAutoLogon.Name = "sbpAutoLogon"
        Me.sbpAutoLogon.Text = "자동 로그아웃 OFF"
        Me.sbpAutoLogon.Width = 120
        '
        'mmnuLIS
        '
        Me.mmnuLIS.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuO, Me.mnuC, Me.mnuJ, Me.mnuR, Me.mnuS, Me.mnuB00, Me.mnuM, Me.mnuQ00, Me.mnuF00, Me.mnuWindow, Me.mnuMediack})
        '
        'mnuO
        '
        Me.mnuO.Index = 0
        Me.mnuO.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuO_input, Me.mnuO_srch, Me.mnuO_hr, Me.mnuO_ord, Me.mnuO_custlist, Me.mnuO_base, Me.mnuO_test, Me.mnuO_Ncov})
        Me.mnuO.Tag = ""
        Me.mnuO.Text = "처방 및 수탁관리(&O)"
        '
        'mnuO_input
        '
        Me.mnuO_input.Index = 0
        Me.mnuO_input.Tag = ""
        Me.mnuO_input.Text = "처방입력"
        '
        'mnuO_srch
        '
        Me.mnuO_srch.Index = 1
        Me.mnuO_srch.Tag = ""
        Me.mnuO_srch.Text = "처방내역"
        '
        'mnuO_hr
        '
        Me.mnuO_hr.Index = 2
        Me.mnuO_hr.Text = "-"
        '
        'mnuO_ord
        '
        Me.mnuO_ord.Index = 3
        Me.mnuO_ord.Text = "수탁처방 입력"
        '
        'mnuO_custlist
        '
        Me.mnuO_custlist.Index = 4
        Me.mnuO_custlist.Text = "수탁검사 거래명세서"
        '
        'mnuO_base
        '
        Me.mnuO_base.Index = 5
        Me.mnuO_base.Text = "수탁관련 기초코드"
        '
        'mnuO_test
        '
        Me.mnuO_test.Index = 6
        Me.mnuO_test.Text = "테스트"
        '
        'mnuO_Ncov
        '
        Me.mnuO_Ncov.Index = 7
        Me.mnuO_Ncov.Text = "신종코로나 결과전송"
        '
        'mnuC
        '
        Me.mnuC.Index = 1
        Me.mnuC.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuC_lab, Me.mnuC_ward, Me.mnuC_cust, Me.mnuC_out, Me.mnuC_ward_batch, Me.mnuC_pis_out, Me.mnuC_pis_ward})
        Me.mnuC.Text = "채혈관리(&C)"
        '
        'mnuC_lab
        '
        Me.mnuC_lab.Index = 0
        Me.mnuC_lab.Tag = ""
        Me.mnuC_lab.Text = "외래채혈"
        '
        'mnuC_ward
        '
        Me.mnuC_ward.Index = 1
        Me.mnuC_ward.Tag = ""
        Me.mnuC_ward.Text = "병동채혈"
        '
        'mnuC_cust
        '
        Me.mnuC_cust.Index = 2
        Me.mnuC_cust.Text = "수탁검사 채혈"
        '
        'mnuC_out
        '
        Me.mnuC_out.Index = 3
        Me.mnuC_out.Text = "외래 간호채혈"
        '
        'mnuC_ward_batch
        '
        Me.mnuC_ward_batch.Index = 4
        Me.mnuC_ward_batch.Text = "병동 일괄 채혈"
        '
        'mnuC_pis_out
        '
        Me.mnuC_pis_out.Index = 5
        Me.mnuC_pis_out.Text = "병리검체채취(외래)"
        '
        'mnuC_pis_ward
        '
        Me.mnuC_pis_ward.Index = 6
        Me.mnuC_pis_ward.Text = "병리검체채취(병동)"
        '
        'mnuJ
        '
        Me.mnuJ.Index = 2
        Me.mnuJ.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuJ_pass, Me.mnuJ_PassTake, Me.mnuJ_take, Me.mnuJ_cancel, Me.mnuJ_bc_reprint, Me.mnuJ_take2, Me.mnuJ_tk_icu, Me.mnuJ_hr, Me.mnuJ_wl, Me.mnuJ_ExLab})
        Me.mnuJ.Text = "접수관리(&J)"
        '
        'mnuJ_pass
        '
        Me.mnuJ_pass.Index = 0
        Me.mnuJ_pass.Text = "검체전달"
        '
        'mnuJ_PassTake
        '
        Me.mnuJ_PassTake.Index = 1
        Me.mnuJ_PassTake.Text = "검체전달 및 접수"
        '
        'mnuJ_take
        '
        Me.mnuJ_take.Index = 2
        Me.mnuJ_take.Tag = ""
        Me.mnuJ_take.Text = "검체접수"
        '
        'mnuJ_cancel
        '
        Me.mnuJ_cancel.Index = 3
        Me.mnuJ_cancel.Tag = ""
        Me.mnuJ_cancel.Text = "채혈/접수 취소"
        '
        'mnuJ_bc_reprint
        '
        Me.mnuJ_bc_reprint.Index = 4
        Me.mnuJ_bc_reprint.Tag = ""
        Me.mnuJ_bc_reprint.Text = "바코드 재출력"
        '
        'mnuJ_take2
        '
        Me.mnuJ_take2.Index = 5
        Me.mnuJ_take2.Text = "부서별 검체접수"
        '
        'mnuJ_tk_icu
        '
        Me.mnuJ_tk_icu.Index = 6
        Me.mnuJ_tk_icu.Text = "검체접수(ICU)"
        '
        'mnuJ_hr
        '
        Me.mnuJ_hr.Index = 7
        Me.mnuJ_hr.Text = "-"
        '
        'mnuJ_wl
        '
        Me.mnuJ_wl.Index = 8
        Me.mnuJ_wl.Text = "W/L 생성 및 조회"
        '
        'mnuJ_ExLab
        '
        Me.mnuJ_ExLab.Index = 9
        Me.mnuJ_ExLab.Text = "위탁검사 리스트 작성"
        '
        'mnuR
        '
        Me.mnuR.Index = 3
        Me.mnuR.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuR_sample, Me.mnuR_labusr, Me.mnuR_item, Me.mnuR_wl, Me.mnuR_exlab, Me.mnuR_exlab_scl, Me.mnuR_hyph01, Me.mnuR_special, Me.mnuR_icu, Me.mnuR_poct, Me.mnuR_hyph02, Me.mnuR_ks, Me.mnuR_chg_regno, Me.MenuItem1, Me.MenuItem2, Me.mnuR_exlab_sml, Me.mnuR_exlab_gcl})
        Me.mnuR.Text = "결과관리(&R)"
        '
        'mnuR_sample
        '
        Me.mnuR_sample.Index = 0
        Me.mnuR_sample.Tag = ""
        Me.mnuR_sample.Text = "분야별 결과저장 및 보고"
        '
        'mnuR_labusr
        '
        Me.mnuR_labusr.Index = 1
        Me.mnuR_labusr.Tag = ""
        Me.mnuR_labusr.Text = "담당자별 결과저장 및 보고"
        '
        'mnuR_item
        '
        Me.mnuR_item.Index = 2
        Me.mnuR_item.Tag = ""
        Me.mnuR_item.Text = "검사항목별 결과저장 및 보고"
        '
        'mnuR_wl
        '
        Me.mnuR_wl.Index = 3
        Me.mnuR_wl.Text = "W/L 생성별 결과저장 및 보고"
        '
        'mnuR_exlab
        '
        Me.mnuR_exlab.Index = 4
        Me.mnuR_exlab.Tag = ""
        Me.mnuR_exlab.Text = "위탁검사 결과저장 및 보고"
        '
        'mnuR_exlab_scl
        '
        Me.mnuR_exlab_scl.Index = 5
        Me.mnuR_exlab_scl.Text = "위탁검사 결과저장 및 보고 (SCL)"
        '
        'mnuR_hyph01
        '
        Me.mnuR_hyph01.Index = 6
        Me.mnuR_hyph01.Text = "-"
        '
        'mnuR_special
        '
        Me.mnuR_special.Index = 7
        Me.mnuR_special.Tag = ""
        Me.mnuR_special.Text = "특수검사 결과저장 및 보고"
        '
        'mnuR_icu
        '
        Me.mnuR_icu.Index = 8
        Me.mnuR_icu.Text = "ICU(ABGA) 검사결과 저장 및 보고"
        '
        'mnuR_poct
        '
        Me.mnuR_poct.Index = 9
        Me.mnuR_poct.Text = "현장검사 결과저장 및 보고"
        '
        'mnuR_hyph02
        '
        Me.mnuR_hyph02.Index = 10
        Me.mnuR_hyph02.Text = "-"
        '
        'mnuR_ks
        '
        Me.mnuR_ks.Index = 11
        Me.mnuR_ks.Text = "보관 검체 관리(일반)"
        '
        'mnuR_chg_regno
        '
        Me.mnuR_chg_regno.Index = 12
        Me.mnuR_chg_regno.Text = "등록번호 변경"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 13
        Me.MenuItem1.Text = "환경배양 누적 결과조회"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 14
        Me.MenuItem2.Text = "환경배양 검사결과 보고서"
        '
        'mnuR_exlab_sml
        '
        Me.mnuR_exlab_sml.Index = 15
        Me.mnuR_exlab_sml.Text = "위탁검사 결과저장 및 보고 (SML)"
        '
        'mnuR_exlab_gcl
        '
        Me.mnuR_exlab_gcl.Index = 16
        Me.mnuR_exlab_gcl.Text = "위탁검사 결과저장 및 보고 (GCL)"
        '
        'mnuS
        '
        Me.mnuS.Index = 4
        Me.mnuS.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuS_tk_list, Me.mnuS_wklist, Me.mnuS_rsheet, Me.mnuS_abn, Me.mnuS_abn2, Me.mnuS_ordstate, Me.mnuS_ordhistory, Me.mnuS_NotColl, Me.mnuS_clist, Me.mnuS_UnfitSpc, Me.mnuS_abn_rst, Me.mnuS_ReTest, Me.mnuS_FnDataCmt, Me.mnuS_tat, Me.mnuS_tat_tcls, Me.mnuS_stcoll, Me.mnuS_st, Me.mnuS_st_dr, Me.mnuS_st_dept, Me.mnuS_sttat, Me.mnuS_coll_st, Me.mnuS_st_fn, Me.mnuS_Hos, Me.mnuS_hyph01, Me.mnuS_rvo, Me.mnuS_rvt, Me.mnuS_rv_a, Me.mnuS_rv_u, Me.mnuS_st_spc, Me.mnuS_exec, Me.mnuS_Hos2, Me.mnuS_rstcnt})
        Me.mnuS.Text = "조회(&S)"
        '
        'mnuS_tk_list
        '
        Me.mnuS_tk_list.Index = 0
        Me.mnuS_tk_list.Text = "채혈 및 접수대장"
        '
        'mnuS_wklist
        '
        Me.mnuS_wklist.Index = 1
        Me.mnuS_wklist.Text = "WorkList 조회 및 인쇄"
        '
        'mnuS_rsheet
        '
        Me.mnuS_rsheet.Index = 2
        Me.mnuS_rsheet.Text = "결과대장"
        '
        'mnuS_abn
        '
        Me.mnuS_abn.Index = 3
        Me.mnuS_abn.Text = "이상자 조회"
        '
        'mnuS_abn2
        '
        Me.mnuS_abn2.Index = 4
        Me.mnuS_abn2.Text = "이상자 분석(결과값 조회)"
        '
        'mnuS_ordstate
        '
        Me.mnuS_ordstate.Index = 5
        Me.mnuS_ordstate.Text = "환자/검체 상태 조회"
        '
        'mnuS_ordhistory
        '
        Me.mnuS_ordhistory.Index = 6
        Me.mnuS_ordhistory.Text = "환자/검체 History 조회"
        '
        'mnuS_NotColl
        '
        Me.mnuS_NotColl.Index = 7
        Me.mnuS_NotColl.Text = "미채혈 사유 대장"
        '
        'mnuS_clist
        '
        Me.mnuS_clist.Index = 8
        Me.mnuS_clist.Text = "채혈/접수 취소 내역 조회"
        '
        'mnuS_UnfitSpc
        '
        Me.mnuS_UnfitSpc.Index = 9
        Me.mnuS_UnfitSpc.Text = "부적합검체 조회"
        '
        'mnuS_abn_rst
        '
        Me.mnuS_abn_rst.Index = 10
        Me.mnuS_abn_rst.Text = "특이결과 조회"
        '
        'mnuS_ReTest
        '
        Me.mnuS_ReTest.Index = 11
        Me.mnuS_ReTest.Text = "재검 내역 조회"
        '
        'mnuS_FnDataCmt
        '
        Me.mnuS_FnDataCmt.Index = 12
        Me.mnuS_FnDataCmt.Text = "최종보고 수정사유 조회"
        '
        'mnuS_tat
        '
        Me.mnuS_tat.Index = 13
        Me.mnuS_tat.Text = "TurnAroundTime 조회"
        '
        'mnuS_tat_tcls
        '
        Me.mnuS_tat_tcls.Index = 14
        Me.mnuS_tat_tcls.Text = "TurnAroundTime 관리"
        '
        'mnuS_stcoll
        '
        Me.mnuS_stcoll.Index = 15
        Me.mnuS_stcoll.Text = "채혈통계 조회"
        '
        'mnuS_st
        '
        Me.mnuS_st.Index = 16
        Me.mnuS_st.Text = "검사통계 조회"
        '
        'mnuS_st_dr
        '
        Me.mnuS_st_dr.Index = 17
        Me.mnuS_st_dr.Text = "검사통계(처방의사별) 조회"
        '
        'mnuS_st_dept
        '
        Me.mnuS_st_dept.Index = 18
        Me.mnuS_st_dept.Text = "검사통계(진료과별) 조회"
        '
        'mnuS_sttat
        '
        Me.mnuS_sttat.Index = 19
        Me.mnuS_sttat.Text = "TurnAroundTime 통계"
        '
        'mnuS_coll_st
        '
        Me.mnuS_coll_st.Index = 20
        Me.mnuS_coll_st.Text = "채혈실통계(시간대별) 조회"
        '
        'mnuS_st_fn
        '
        Me.mnuS_st_fn.Index = 21
        Me.mnuS_st_fn.Text = "최종보고 수정률 통계 조회"
        '
        'mnuS_Hos
        '
        Me.mnuS_Hos.Index = 22
        Me.mnuS_Hos.Text = "병원체 검사결과 신고"
        '
        'mnuS_hyph01
        '
        Me.mnuS_hyph01.Index = 23
        Me.mnuS_hyph01.Text = "-"
        '
        'mnuS_rvo
        '
        Me.mnuS_rvo.Index = 24
        Me.mnuS_rvo.Text = "결과조회(처방일자별)"
        '
        'mnuS_rvt
        '
        Me.mnuS_rvt.Index = 25
        Me.mnuS_rvt.Text = "결과조회(일일보고서)"
        '
        'mnuS_rv_a
        '
        Me.mnuS_rv_a.Index = 26
        Me.mnuS_rv_a.Text = "누적 검사결과 조회"
        '
        'mnuS_rv_u
        '
        Me.mnuS_rv_u.Index = 27
        Me.mnuS_rv_u.Text = "통합결과 조회"
        '
        'mnuS_st_spc
        '
        Me.mnuS_st_spc.Index = 28
        Me.mnuS_st_spc.Text = "검체통계 조회"
        '
        'mnuS_exec
        '
        Me.mnuS_exec.Index = 29
        Me.mnuS_exec.Text = "실시확인비교"
        '
        'mnuS_Hos2
        '
        Me.mnuS_Hos2.Index = 30
        Me.mnuS_Hos2.Text = "병원체 검사결과 신고(자동신고)"
        '
        'mnuS_rstcnt
        '
        Me.mnuS_rstcnt.Index = 31
        Me.mnuS_rstcnt.Text = "결과값 통계"
        '
        'mnuB00
        '
        Me.mnuB00.Index = 5
        Me.mnuB00.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuB015, Me.mnuB_hr0, Me.mnuB_tns_jubsu, Me.mnuB_bef_reg, Me.mnuB_out, Me.mnuB_abn, Me.mnuB_abn_part, Me.mnuB_keep, Me.mnuB_bef_cancel, Me.mnuB_hr1, Me.mnuB_bld_history, Me.mnuB_tns_list, Me.mnuB047, Me.mnuB_abn_list, Me.mnuB_bld_io_st, Me.mnuB049, Me.mnuB_abn_st, Me.mnuB94, Me.mnuB_outabn_dept, Me.mnuB_XMatch_cnt, Me.mnuB048, Me.mnuB_hr2, Me.mnuB_bcno_rst, Me.mnuB_test_rst, Me.mnuB_abo_rst, Me.mnuB_aborh_rst, Me.mnuB_cross_rst, Me.mnuB_aborh_prt, Me.mnuB_hr3, Me.mnuB_op, Me.mnuB_IO})
        Me.mnuB00.Text = "혈액은행(&B)"
        '
        'mnuB015
        '
        Me.mnuB015.Index = 0
        Me.mnuB015.Text = "혈액 입고"
        '
        'mnuB_hr0
        '
        Me.mnuB_hr0.Index = 1
        Me.mnuB_hr0.Text = "-"
        '
        'mnuB_tns_jubsu
        '
        Me.mnuB_tns_jubsu.Index = 2
        Me.mnuB_tns_jubsu.Text = "수혈 의뢰 접수"
        '
        'mnuB_bef_reg
        '
        Me.mnuB_bef_reg.Index = 3
        Me.mnuB_bef_reg.Text = "Cross Matching 등록(가출고)"
        '
        'mnuB_out
        '
        Me.mnuB_out.Index = 4
        Me.mnuB_out.Text = "혈액 출고"
        '
        'mnuB_abn
        '
        Me.mnuB_abn.Index = 5
        Me.mnuB_abn.Text = "혈액 반납/폐기"
        '
        'mnuB_abn_part
        '
        Me.mnuB_abn_part.Index = 6
        Me.mnuB_abn_part.Text = "혈액 자체폐기/교환"
        '
        'mnuB_keep
        '
        Me.mnuB_keep.Index = 7
        Me.mnuB_keep.Text = "보관 검체 관리"
        '
        'mnuB_bef_cancel
        '
        Me.mnuB_bef_cancel.Index = 8
        Me.mnuB_bef_cancel.Text = "가출고 취소"
        '
        'mnuB_hr1
        '
        Me.mnuB_hr1.Index = 9
        Me.mnuB_hr1.Text = "-"
        '
        'mnuB_bld_history
        '
        Me.mnuB_bld_history.Index = 10
        Me.mnuB_bld_history.Text = "혈액 이력 조회"
        '
        'mnuB_tns_list
        '
        Me.mnuB_tns_list.Index = 11
        Me.mnuB_tns_list.Text = "수혈 의뢰 현황 조회"
        '
        'mnuB047
        '
        Me.mnuB047.Index = 12
        Me.mnuB047.Text = "분리혈액 조회"
        Me.mnuB047.Visible = False
        '
        'mnuB_abn_list
        '
        Me.mnuB_abn_list.Index = 13
        Me.mnuB_abn_list.Text = "혈액 반납/폐기 리스트 조회"
        '
        'mnuB_bld_io_st
        '
        Me.mnuB_bld_io_st.Index = 14
        Me.mnuB_bld_io_st.Text = "혈액 입고/출고 현황 조회"
        '
        'mnuB049
        '
        Me.mnuB049.Index = 15
        Me.mnuB049.Text = "혈액 입고/출고 월별 조회"
        '
        'mnuB_abn_st
        '
        Me.mnuB_abn_st.Index = 16
        Me.mnuB_abn_st.Text = "혈액 반납/폐기 건수 조회"
        '
        'mnuB94
        '
        Me.mnuB94.Index = 17
        Me.mnuB94.Text = "혈액 반납/폐기율 조회"
        '
        'mnuB_outabn_dept
        '
        Me.mnuB_outabn_dept.Index = 18
        Me.mnuB_outabn_dept.Text = "혈액 출고/폐기 진료과별 현황 조회"
        '
        'mnuB_XMatch_cnt
        '
        Me.mnuB_XMatch_cnt.Index = 19
        Me.mnuB_XMatch_cnt.Text = "혈액 X-Matching 진료과별 현황"
        '
        'mnuB048
        '
        Me.mnuB048.Index = 20
        Me.mnuB048.Text = "혈액 재고량 조회"
        '
        'mnuB_hr2
        '
        Me.mnuB_hr2.Index = 21
        Me.mnuB_hr2.Text = "-"
        '
        'mnuB_bcno_rst
        '
        Me.mnuB_bcno_rst.Index = 22
        Me.mnuB_bcno_rst.Text = "분야별 결과저장 및 보고(T)"
        '
        'mnuB_test_rst
        '
        Me.mnuB_test_rst.Index = 23
        Me.mnuB_test_rst.Text = "검사항목별 결과저장 및 보고(T)"
        '
        'mnuB_abo_rst
        '
        Me.mnuB_abo_rst.Index = 24
        Me.mnuB_abo_rst.Text = "혈액형 결과수정 및 보고(T)"
        '
        'mnuB_aborh_rst
        '
        Me.mnuB_aborh_rst.Index = 25
        Me.mnuB_aborh_rst.Text = "혈액형 2차 결과 등록"
        '
        'mnuB_cross_rst
        '
        Me.mnuB_cross_rst.Index = 26
        Me.mnuB_cross_rst.Text = "Cross Matching 결과 수정"
        '
        'mnuB_aborh_prt
        '
        Me.mnuB_aborh_prt.Index = 27
        Me.mnuB_aborh_prt.Text = "혈액형 결과대장"
        '
        'mnuB_hr3
        '
        Me.mnuB_hr3.Index = 28
        Me.mnuB_hr3.Text = "-"
        '
        'mnuB_op
        '
        Me.mnuB_op.Index = 29
        Me.mnuB_op.Text = "수술환자 확정 조회"
        '
        'mnuB_IO
        '
        Me.mnuB_IO.Index = 30
        Me.mnuB_IO.Text = "질병관리본부 입/출고 관리"
        '
        'mnuM
        '
        Me.mnuM.Index = 6
        Me.mnuM.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuM_sample, Me.mnuM_item, Me.mnuM_ng, Me.mnuM_hr0, Me.mnuM_wl, Me.mnuM_wl_exe, Me.mnuM_gr, Me.mnuM_abn, Me.mnuM_rstsheet, Me.mnuM_statistics, Me.mnuM_hr1, Me.mnuM_ks})
        Me.mnuM.Text = "미생물(&M)"
        '
        'mnuM_sample
        '
        Me.mnuM_sample.Index = 0
        Me.mnuM_sample.Text = "분야별 결과저장 및 보고 (M)"
        '
        'mnuM_item
        '
        Me.mnuM_item.Index = 1
        Me.mnuM_item.Text = "검사항목별 결과저장 및 보고 (M)"
        '
        'mnuM_ng
        '
        Me.mnuM_ng.Index = 2
        Me.mnuM_ng.Text = "No growth 결과저장 및 보고"
        '
        'mnuM_hr0
        '
        Me.mnuM_hr0.Index = 3
        Me.mnuM_hr0.Text = "-"
        '
        'mnuM_wl
        '
        Me.mnuM_wl.Index = 4
        Me.mnuM_wl.Text = "WorkList 조회 및 인쇄(미생물)"
        '
        'mnuM_wl_exe
        '
        Me.mnuM_wl_exe.Index = 5
        Me.mnuM_wl_exe.Text = "W/L 생성 및 조회(미생물)"
        '
        'mnuM_gr
        '
        Me.mnuM_gr.Index = 6
        Me.mnuM_gr.Text = "양성자 조회"
        '
        'mnuM_abn
        '
        Me.mnuM_abn.Index = 7
        Me.mnuM_abn.Text = "검사결과 조회"
        '
        'mnuM_rstsheet
        '
        Me.mnuM_rstsheet.Index = 8
        Me.mnuM_rstsheet.Text = "미생물 결과대장"
        '
        'mnuM_statistics
        '
        Me.mnuM_statistics.Index = 9
        Me.mnuM_statistics.Text = "미생물 통계 조회"
        '
        'mnuM_hr1
        '
        Me.mnuM_hr1.Index = 10
        Me.mnuM_hr1.Tag = ""
        Me.mnuM_hr1.Text = "-"
        '
        'mnuM_ks
        '
        Me.mnuM_ks.Index = 11
        Me.mnuM_ks.Text = "보관 검체 관리(M)"
        '
        'mnuQ00
        '
        Me.mnuQ00.Index = 7
        Me.mnuQ00.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuQ01, Me.mnuQ02, Me.mnuQ03})
        Me.mnuQ00.Text = "검사실인증(&Q)"
        '
        'mnuQ01
        '
        Me.mnuQ01.Index = 0
        Me.mnuQ01.Text = "정도관리(QC)"
        '
        'mnuQ02
        '
        Me.mnuQ02.Index = 1
        Me.mnuQ02.Text = "종합검증"
        '
        'mnuQ03
        '
        Me.mnuQ03.Index = 2
        Me.mnuQ03.Text = "이미지 일괄등록"
        '
        'mnuF00
        '
        Me.mnuF00.Index = 8
        Me.mnuF00.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuF01, Me.mnuF_test, Me.MnuRef})
        Me.mnuF00.Text = "기초코드관리(&F)"
        '
        'mnuF01
        '
        Me.mnuF01.Index = 0
        Me.mnuF01.Text = "기초마스터 관리"
        '
        'mnuF_test
        '
        Me.mnuF_test.Index = 1
        Me.mnuF_test.Text = "검사코드 등록"
        '
        'MnuRef
        '
        Me.MnuRef.Index = 2
        Me.MnuRef.Text = "병원체코드 관리"
        '
        'mnuWindow
        '
        Me.mnuWindow.Index = 9
        Me.mnuWindow.MdiList = True
        Me.mnuWindow.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuCascade, Me.mnuTileHorizontal, Me.mnuTileVertical})
        Me.mnuWindow.Text = "창(&W)"
        '
        'mnuCascade
        '
        Me.mnuCascade.Index = 0
        Me.mnuCascade.Text = "계단식 창 배열(&S)"
        '
        'mnuTileHorizontal
        '
        Me.mnuTileHorizontal.Index = 1
        Me.mnuTileHorizontal.Text = "가로 바둑판식 창 배열(&H)"
        '
        'mnuTileVertical
        '
        Me.mnuTileVertical.Index = 2
        Me.mnuTileVertical.Text = "세로 바둑판식 창 배열(&V)"
        '
        'mnuMediack
        '
        Me.mnuMediack.Index = 10
        Me.mnuMediack.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAbout, Me.mnuHelp, Me.mnu_hr1, Me.mnuSet_EmrPrt, Me.mnu_hr2, Me.mnuAS, Me.mnu_hr3, Me.mnuExit, Me.MnuTest2})
        Me.mnuMediack.Text = "MEDI@CK"
        '
        'mnuAbout
        '
        Me.mnuAbout.Index = 0
        Me.mnuAbout.Text = "MEDI@CK 정보(&A)"
        '
        'mnuHelp
        '
        Me.mnuHelp.Index = 1
        Me.mnuHelp.Text = "LIS 검사정보"
        '
        'mnu_hr1
        '
        Me.mnu_hr1.Index = 2
        Me.mnu_hr1.Text = "-"
        '
        'mnuSet_EmrPrt
        '
        Me.mnuSet_EmrPrt.Index = 3
        Me.mnuSet_EmrPrt.Text = "ERM 이미지 프린트 설정"
        '
        'mnu_hr2
        '
        Me.mnu_hr2.Index = 4
        Me.mnu_hr2.Text = "-"
        '
        'mnuAS
        '
        Me.mnuAS.Index = 5
        Me.mnuAS.Text = "A/S 접수 및 현황"
        '
        'mnu_hr3
        '
        Me.mnu_hr3.Index = 6
        Me.mnu_hr3.Text = "-"
        '
        'mnuExit
        '
        Me.mnuExit.Index = 7
        Me.mnuExit.Text = "프로그램 종료"
        '
        'MnuTest2
        '
        Me.MnuTest2.Index = 8
        Me.MnuTest2.Text = "LIS 검사정보(데모)"
        Me.MnuTest2.Visible = False
        '
        'MenuItem18
        '
        Me.MenuItem18.Index = -1
        Me.MenuItem18.Text = ""
        '
        'pnlProgress
        '
        Me.pnlProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlProgress.Controls.Add(Me.pbrMain)
        Me.pnlProgress.Location = New System.Drawing.Point(84, 726)
        Me.pnlProgress.Name = "pnlProgress"
        Me.pnlProgress.Size = New System.Drawing.Size(779, 22)
        Me.pnlProgress.TabIndex = 13
        Me.pnlProgress.Visible = False
        '
        'pbrMain
        '
        Me.pbrMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbrMain.Location = New System.Drawing.Point(0, 0)
        Me.pbrMain.Name = "pbrMain"
        Me.pbrMain.Size = New System.Drawing.Size(779, 22)
        Me.pbrMain.TabIndex = 0
        '
        'tmrNowDateTime
        '
        Me.tmrNowDateTime.Interval = 1000
        '
        'tmrAutoUpdate
        '
        Me.tmrAutoUpdate.Enabled = True
        Me.tmrAutoUpdate.Interval = 14400000
        '
        'niTray
        '
        Me.niTray.Icon = CType(resources.GetObject("niTray.Icon"), System.Drawing.Icon)
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.Controls.Add(Me.btnTabClose)
        Me.Panel1.Controls.Add(Me.tbcMenu)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 40)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1272, 25)
        Me.Panel1.TabIndex = 15
        '
        'btnTabClose
        '
        Me.btnTabClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTabClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnTabClose.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnTabClose.Location = New System.Drawing.Point(1252, 1)
        Me.btnTabClose.Margin = New System.Windows.Forms.Padding(1)
        Me.btnTabClose.Name = "btnTabClose"
        Me.btnTabClose.Size = New System.Drawing.Size(20, 21)
        Me.btnTabClose.TabIndex = 37
        Me.btnTabClose.Text = "x"
        Me.btnTabClose.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnTabClose.UseVisualStyleBackColor = True
        '
        'tbcMenu
        '
        Me.tbcMenu.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbcMenu.Controls.Add(Me.TabPage1)
        Me.tbcMenu.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tbcMenu.HotTrack = True
        Me.tbcMenu.Location = New System.Drawing.Point(0, 1)
        Me.tbcMenu.Margin = New System.Windows.Forms.Padding(1)
        Me.tbcMenu.Name = "tbcMenu"
        Me.tbcMenu.SelectedIndex = 0
        Me.tbcMenu.ShowToolTips = True
        Me.tbcMenu.Size = New System.Drawing.Size(1252, 25)
        Me.tbcMenu.TabIndex = 26
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = True
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(1244, 0)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ImageList
        '
        Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList.Images.SetKeyName(0, "검사실인증.gif")
        Me.ImageList.Images.SetKeyName(1, "검체.gif")
        Me.ImageList.Images.SetKeyName(2, "결과.gif")
        Me.ImageList.Images.SetKeyName(3, "마스터.gif")
        Me.ImageList.Images.SetKeyName(4, "물품.gif")
        Me.ImageList.Images.SetKeyName(5, "조회 복사.gif")
        Me.ImageList.Images.SetKeyName(6, "채혈2.gif")
        Me.ImageList.Images.SetKeyName(7, "혈액은행.gif")
        Me.ImageList.Images.SetKeyName(8, "icon0.jpg")
        Me.ImageList.Images.SetKeyName(9, "icon1.jpg")
        Me.ImageList.Images.SetKeyName(10, "icon2.jpg")
        Me.ImageList.Images.SetKeyName(11, "icon3.jpg")
        Me.ImageList.Images.SetKeyName(12, "icon4.jpg")
        Me.ImageList.Images.SetKeyName(13, "icon5.jpg")
        Me.ImageList.Images.SetKeyName(14, "icon6.jpg")
        '
        'btnAutoLogon
        '
        Me.btnAutoLogon.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAutoLogon.Location = New System.Drawing.Point(1134, 671)
        Me.btnAutoLogon.Name = "btnAutoLogon"
        Me.btnAutoLogon.Size = New System.Drawing.Size(119, 23)
        Me.btnAutoLogon.TabIndex = 17
        Me.btnAutoLogon.Text = "자동 로그아웃 OFF"
        Me.btnAutoLogon.UseVisualStyleBackColor = True
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlTop.BackgroundImage = CType(resources.GetObject("pnlTop.BackgroundImage"), System.Drawing.Image)
        Me.pnlTop.Controls.Add(Me.btnLock)
        Me.pnlTop.Controls.Add(Me.btnHotm09)
        Me.pnlTop.Controls.Add(Me.btnHotm08)
        Me.pnlTop.Controls.Add(Me.btnHotm07)
        Me.pnlTop.Controls.Add(Me.btnHotm06)
        Me.pnlTop.Controls.Add(Me.btnHotm05)
        Me.pnlTop.Controls.Add(Me.btnHotm04)
        Me.pnlTop.Controls.Add(Me.btnHotm03)
        Me.pnlTop.Controls.Add(Me.btnHotm02)
        Me.pnlTop.Controls.Add(Me.btnHotm01)
        Me.pnlTop.Controls.Add(Me.btnHotm00)
        Me.pnlTop.Controls.Add(Me.btnHotList)
        Me.pnlTop.Controls.Add(Me.btnTray)
        Me.pnlTop.Controls.Add(Me.picLogo)
        Me.pnlTop.Controls.Add(Me.lblVer)
        Me.pnlTop.Controls.Add(Me.btnLogout)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1272, 40)
        Me.pnlTop.TabIndex = 11
        '
        'btnLock
        '
        Me.btnLock.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLock.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnLock.BackgroundImage = CType(resources.GetObject("btnLock.BackgroundImage"), System.Drawing.Image)
        Me.btnLock.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker1.IsActive = True
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnLock.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.White, System.Drawing.Color.White}
        CBlendItems1.iPoint = New Single() {0!, 1.0!}
        Me.btnLock.ColorFillBlend = CBlendItems1
        Me.btnLock.ColorFillSolid = System.Drawing.Color.Plum
        Me.btnLock.Corners.All = CType(0, Short)
        Me.btnLock.Corners.LowerLeft = CType(0, Short)
        Me.btnLock.Corners.LowerRight = CType(0, Short)
        Me.btnLock.Corners.UpperLeft = CType(0, Short)
        Me.btnLock.Corners.UpperRight = CType(0, Short)
        Me.btnLock.FillType = CButtonLib.CButton.eFillType.Solid
        Me.btnLock.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnLock.FocalPoints.CenterPtX = 0.02777778!
        Me.btnLock.FocalPoints.CenterPtY = 0.5238096!
        Me.btnLock.FocalPoints.FocusPtX = 0.04166667!
        Me.btnLock.FocalPoints.FocusPtY = 0.1428571!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnLock.FocusPtTracker = DesignerRectTracker2
        Me.btnLock.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnLock.ForeColor = System.Drawing.Color.White
        Me.btnLock.Image = Nothing
        Me.btnLock.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnLock.ImageIndex = 0
        Me.btnLock.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnLock.Location = New System.Drawing.Point(1123, 10)
        Me.btnLock.Margin = New System.Windows.Forms.Padding(1)
        Me.btnLock.Name = "btnLock"
        Me.btnLock.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnLock.SideImage = Nothing
        Me.btnLock.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnLock.Size = New System.Drawing.Size(72, 21)
        Me.btnLock.TabIndex = 47
        Me.btnLock.Text = "잠금"
        Me.btnLock.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLock.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnLock.TextMargin = New System.Windows.Forms.Padding(0)
        Me.btnLock.Visible = False
        '
        'btnHotm09
        '
        Me.btnHotm09.AutoSize = True
        Me.btnHotm09.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHotm09.FlatAppearance.BorderSize = 0
        Me.btnHotm09.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHotm09.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnHotm09.Location = New System.Drawing.Point(315, 8)
        Me.btnHotm09.Margin = New System.Windows.Forms.Padding(1)
        Me.btnHotm09.Name = "btnHotm09"
        Me.btnHotm09.Size = New System.Drawing.Size(19, 20)
        Me.btnHotm09.TabIndex = 46
        Me.btnHotm09.UseVisualStyleBackColor = True
        Me.btnHotm09.Visible = False
        '
        'btnHotm08
        '
        Me.btnHotm08.AutoSize = True
        Me.btnHotm08.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHotm08.FlatAppearance.BorderSize = 0
        Me.btnHotm08.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHotm08.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnHotm08.Location = New System.Drawing.Point(294, 8)
        Me.btnHotm08.Margin = New System.Windows.Forms.Padding(1)
        Me.btnHotm08.Name = "btnHotm08"
        Me.btnHotm08.Size = New System.Drawing.Size(19, 20)
        Me.btnHotm08.TabIndex = 45
        Me.btnHotm08.UseVisualStyleBackColor = True
        Me.btnHotm08.Visible = False
        '
        'btnHotm07
        '
        Me.btnHotm07.AutoSize = True
        Me.btnHotm07.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHotm07.FlatAppearance.BorderSize = 0
        Me.btnHotm07.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHotm07.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnHotm07.Location = New System.Drawing.Point(273, 8)
        Me.btnHotm07.Margin = New System.Windows.Forms.Padding(1)
        Me.btnHotm07.Name = "btnHotm07"
        Me.btnHotm07.Size = New System.Drawing.Size(19, 20)
        Me.btnHotm07.TabIndex = 44
        Me.btnHotm07.UseVisualStyleBackColor = True
        Me.btnHotm07.Visible = False
        '
        'btnHotm06
        '
        Me.btnHotm06.AutoSize = True
        Me.btnHotm06.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHotm06.FlatAppearance.BorderSize = 0
        Me.btnHotm06.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHotm06.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnHotm06.Location = New System.Drawing.Point(252, 8)
        Me.btnHotm06.Margin = New System.Windows.Forms.Padding(1)
        Me.btnHotm06.Name = "btnHotm06"
        Me.btnHotm06.Size = New System.Drawing.Size(19, 20)
        Me.btnHotm06.TabIndex = 43
        Me.btnHotm06.UseVisualStyleBackColor = True
        Me.btnHotm06.Visible = False
        '
        'btnHotm05
        '
        Me.btnHotm05.AutoSize = True
        Me.btnHotm05.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHotm05.FlatAppearance.BorderSize = 0
        Me.btnHotm05.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHotm05.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnHotm05.Location = New System.Drawing.Point(231, 8)
        Me.btnHotm05.Margin = New System.Windows.Forms.Padding(1)
        Me.btnHotm05.Name = "btnHotm05"
        Me.btnHotm05.Size = New System.Drawing.Size(19, 20)
        Me.btnHotm05.TabIndex = 42
        Me.btnHotm05.UseVisualStyleBackColor = True
        Me.btnHotm05.Visible = False
        '
        'btnHotm04
        '
        Me.btnHotm04.AutoSize = True
        Me.btnHotm04.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHotm04.FlatAppearance.BorderSize = 0
        Me.btnHotm04.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHotm04.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnHotm04.Location = New System.Drawing.Point(210, 8)
        Me.btnHotm04.Margin = New System.Windows.Forms.Padding(1)
        Me.btnHotm04.Name = "btnHotm04"
        Me.btnHotm04.Size = New System.Drawing.Size(19, 20)
        Me.btnHotm04.TabIndex = 41
        Me.btnHotm04.UseVisualStyleBackColor = True
        Me.btnHotm04.Visible = False
        '
        'btnHotm03
        '
        Me.btnHotm03.AutoSize = True
        Me.btnHotm03.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHotm03.FlatAppearance.BorderSize = 0
        Me.btnHotm03.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHotm03.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnHotm03.Location = New System.Drawing.Point(189, 8)
        Me.btnHotm03.Margin = New System.Windows.Forms.Padding(1)
        Me.btnHotm03.Name = "btnHotm03"
        Me.btnHotm03.Size = New System.Drawing.Size(19, 20)
        Me.btnHotm03.TabIndex = 40
        Me.btnHotm03.UseVisualStyleBackColor = True
        Me.btnHotm03.Visible = False
        '
        'btnHotm02
        '
        Me.btnHotm02.AutoSize = True
        Me.btnHotm02.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHotm02.FlatAppearance.BorderSize = 0
        Me.btnHotm02.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHotm02.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnHotm02.Location = New System.Drawing.Point(168, 8)
        Me.btnHotm02.Margin = New System.Windows.Forms.Padding(1)
        Me.btnHotm02.Name = "btnHotm02"
        Me.btnHotm02.Size = New System.Drawing.Size(19, 20)
        Me.btnHotm02.TabIndex = 39
        Me.btnHotm02.UseVisualStyleBackColor = True
        Me.btnHotm02.Visible = False
        '
        'btnHotm01
        '
        Me.btnHotm01.AutoSize = True
        Me.btnHotm01.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHotm01.FlatAppearance.BorderSize = 0
        Me.btnHotm01.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHotm01.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnHotm01.Location = New System.Drawing.Point(147, 8)
        Me.btnHotm01.Margin = New System.Windows.Forms.Padding(1)
        Me.btnHotm01.Name = "btnHotm01"
        Me.btnHotm01.Size = New System.Drawing.Size(19, 20)
        Me.btnHotm01.TabIndex = 38
        Me.btnHotm01.UseVisualStyleBackColor = True
        Me.btnHotm01.Visible = False
        '
        'btnHotm00
        '
        Me.btnHotm00.AutoSize = True
        Me.btnHotm00.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnHotm00.FlatAppearance.BorderSize = 0
        Me.btnHotm00.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHotm00.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.btnHotm00.Location = New System.Drawing.Point(126, 8)
        Me.btnHotm00.Margin = New System.Windows.Forms.Padding(1)
        Me.btnHotm00.Name = "btnHotm00"
        Me.btnHotm00.Size = New System.Drawing.Size(19, 20)
        Me.btnHotm00.TabIndex = 37
        Me.btnHotm00.UseVisualStyleBackColor = True
        Me.btnHotm00.Visible = False
        '
        'btnHotList
        '
        Me.btnHotList.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnHotList.BackgroundImage = CType(resources.GetObject("btnHotList.BackgroundImage"), System.Drawing.Image)
        Me.btnHotList.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnHotList.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.White, System.Drawing.Color.White}
        CBlendItems2.iPoint = New Single() {0!, 1.0!}
        Me.btnHotList.ColorFillBlend = CBlendItems2
        Me.btnHotList.ColorFillSolid = System.Drawing.Color.Peru
        Me.btnHotList.Corners.All = CType(0, Short)
        Me.btnHotList.Corners.LowerLeft = CType(0, Short)
        Me.btnHotList.Corners.LowerRight = CType(0, Short)
        Me.btnHotList.Corners.UpperLeft = CType(0, Short)
        Me.btnHotList.Corners.UpperRight = CType(0, Short)
        Me.btnHotList.FillType = CButtonLib.CButton.eFillType.Solid
        Me.btnHotList.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnHotList.FocalPoints.CenterPtX = 0.04201681!
        Me.btnHotList.FocalPoints.CenterPtY = 0.4545455!
        Me.btnHotList.FocalPoints.FocusPtX = 0!
        Me.btnHotList.FocalPoints.FocusPtY = 0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnHotList.FocusPtTracker = DesignerRectTracker4
        Me.btnHotList.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnHotList.ForeColor = System.Drawing.Color.White
        Me.btnHotList.Image = Nothing
        Me.btnHotList.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnHotList.ImageIndex = 0
        Me.btnHotList.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnHotList.Location = New System.Drawing.Point(3, 8)
        Me.btnHotList.Margin = New System.Windows.Forms.Padding(1)
        Me.btnHotList.Name = "btnHotList"
        Me.btnHotList.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnHotList.SideImage = Nothing
        Me.btnHotList.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnHotList.Size = New System.Drawing.Size(119, 21)
        Me.btnHotList.TabIndex = 34
        Me.btnHotList.Text = "즐겨찾기 편집"
        Me.btnHotList.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHotList.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnHotList.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnTray
        '
        Me.btnTray.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTray.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnTray.ForeColor = System.Drawing.Color.White
        Me.btnTray.Location = New System.Drawing.Point(737, 9)
        Me.btnTray.Name = "btnTray"
        Me.btnTray.Size = New System.Drawing.Size(84, 22)
        Me.btnTray.TabIndex = 15
        Me.btnTray.TabStop = False
        '
        'picLogo
        '
        Me.picLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picLogo.Image = CType(resources.GetObject("picLogo.Image"), System.Drawing.Image)
        Me.picLogo.Location = New System.Drawing.Point(823, 9)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(80, 23)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picLogo.TabIndex = 14
        Me.picLogo.TabStop = False
        Me.picLogo.Visible = False
        '
        'lblVer
        '
        Me.lblVer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVer.BackColor = System.Drawing.Color.Transparent
        Me.lblVer.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblVer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblVer.Location = New System.Drawing.Point(979, 15)
        Me.lblVer.Name = "lblVer"
        Me.lblVer.Size = New System.Drawing.Size(140, 14)
        Me.lblVer.TabIndex = 22
        Me.lblVer.Text = "Ver 1.0.1051.9999"
        Me.lblVer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnLogout
        '
        Me.btnLogout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLogout.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnLogout.BackgroundImage = CType(resources.GetObject("btnLogout.BackgroundImage"), System.Drawing.Image)
        Me.btnLogout.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker5.IsActive = True
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnLogout.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.White, System.Drawing.Color.White}
        CBlendItems3.iPoint = New Single() {0!, 1.0!}
        Me.btnLogout.ColorFillBlend = CBlendItems3
        Me.btnLogout.ColorFillSolid = System.Drawing.Color.Plum
        Me.btnLogout.Corners.All = CType(0, Short)
        Me.btnLogout.Corners.LowerLeft = CType(0, Short)
        Me.btnLogout.Corners.LowerRight = CType(0, Short)
        Me.btnLogout.Corners.UpperLeft = CType(0, Short)
        Me.btnLogout.Corners.UpperRight = CType(0, Short)
        Me.btnLogout.FillType = CButtonLib.CButton.eFillType.Solid
        Me.btnLogout.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnLogout.FocalPoints.CenterPtX = 0.02777778!
        Me.btnLogout.FocalPoints.CenterPtY = 0.5238096!
        Me.btnLogout.FocalPoints.FocusPtX = 0.04166667!
        Me.btnLogout.FocalPoints.FocusPtY = 0.1428571!
        DesignerRectTracker6.IsActive = True
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnLogout.FocusPtTracker = DesignerRectTracker6
        Me.btnLogout.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.Image = Nothing
        Me.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnLogout.ImageIndex = 0
        Me.btnLogout.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnLogout.Location = New System.Drawing.Point(1195, 10)
        Me.btnLogout.Margin = New System.Windows.Forms.Padding(1)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnLogout.SideImage = Nothing
        Me.btnLogout.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnLogout.Size = New System.Drawing.Size(72, 21)
        Me.btnLogout.TabIndex = 35
        Me.btnLogout.Text = "로그아웃"
        Me.btnLogout.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnLogout.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'MAIN
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(1272, 696)
        Me.Controls.Add(Me.btnAutoLogon)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlProgress)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.stbMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.Menu = Me.mmnuLIS
        Me.MinimumSize = New System.Drawing.Size(1278, 726)
        Me.Name = "MAIN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MEDI@CK"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.sbpTitleMsg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbpMsg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbpUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbpTitleDt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbpDateTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbpAutoLogon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlProgress.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.tbcMenu.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Shared Sub Main()
        Try
            Application.Run(New MAIN)

        Catch ex As Exception
            MsgBox(ex.Source + " - " + ex.Message, MsgBoxStyle.Exclamation)

        End Try
    End Sub

    Private Sub sbAutoLogin()
        Dim sFn As String = "Private Sub fnAutoLogin()"

        Try
            mbAutoLogin = False
            '초기화
            m_al_Args.Clear()
            m_al_Args.TrimToSize()
            '자동로그인 Arguments
            Dim a_sArgs As String() = System.Environment.GetCommandLineArgs()

            'MsgBox(a_sArgs.Length.ToString)

            If a_sArgs.Length = 1 Then
                'ArgsInfo.txt 파일이 있으면 재로그인
                sbReadInfo()
            Else
                ' AppNm.exe [병동간호 메뉴정의 ID : WARD] [작업구분 : C or R] [사용자ID] [사용자명] [병동코드] [등록번호]
                '      ex) WARD C ACK 관리자 101 18111111

                ' AppNm.exe [외래간호 메뉴정의 ID : OUT ] [작업구분 : C or R] [사용자ID] [사용자명] [진료과코드] [등록번호]
                '      ex) OUT C ACK 관리자 MH 18111111

                ' AppNm.exe [진료과간호 메뉴정의ID: PAT ] [작업구분 : C or R] [사용자ID] [사용자명] [진료과코드] [등록번호]
                '      ex) PAT C ACK 관리자 MH 18111111
                ' 입/외 구분 없이 진료과에 해당하는 등록번호에 대한 환자 조회

                '< 파라미터 정보 로그로 남김 
                Dim sTmp As String = ""

                For ii As Integer = 1 To a_sArgs.Length - 1
                    If sTmp.Length > 0 Then sTmp += " "

                    sTmp += a_sArgs(ii)
                Next
                Fn.logFile("[" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "] " & sTmp, "ParamData_" & Format(Now, "yyyy-MM-dd").ToString, "ParamData")
                '> 

                Dim sArgs As String = ""

                For i As Integer = 2 To a_sArgs.Length
                    If sArgs.Length > 0 Then sArgs += ", "
                    sArgs += a_sArgs(i - 1)
                Next

                Fn.logFile(sArgs, "ArgsDetail")
                Fn.logFile(sArgs, "ArgsInfo")


                sArgs = IO.File.ReadAllText(msPathArgsDetail, System.Text.Encoding.Default)
                Dim sBuf() As String = sArgs.Split

                For i As Integer = 0 To sBuf.Length - 3
                    m_al_Args.Add(sBuf(i).Replace(",", ""))
                Next

                If m_al_Args.Count > 2 Then
                    If m_al_Args(2).ToString = "" Or m_al_Args(3).ToString = "" Then
                        Throw (New Exception("사용자 정보가 존재하지 않습니다." + vbCrLf + vbCrLf + "[" + sArgs + "]"))
                    End If
                End If


                If m_al_Args(1).ToString = "J" Then
                    If m_al_Args.Count < 4 Then Throw (New Exception("파라메터 정보가 정확하지 않습니다." + vbCrLf + vbCrLf + "[" + sArgs + "]"))
                ElseIf m_al_Args(1).ToString = "C" Then
                    If m_al_Args.Count < 5 Then Throw (New Exception("파라메터 정보가 정확하지 않습니다." + vbCrLf + vbCrLf + "[" + sArgs + "]"))
                End If


            End If

            If m_al_Args.Count > 1 Then
                If m_al_Args(1).ToString.Trim = "E" Then
                    If IO.File.Exists(msPathArgsDetail) Then
                        IO.File.Delete(msPathArgsDetail)
                    End If
                    sbKillPreProc()
                    End
                End If
            End If

            Dim iCnt As Integer = 4

            If m_al_Args.Count > 0 Then
                If m_al_Args(1).ToString = "J" Then
                    iCnt = 3
                ElseIf m_al_Args(1).ToString = "L" Then
                    iCnt = 3
                End If
            End If

            If m_al_Args.Count > iCnt Then
                Select Case m_al_Args(0).ToString
                    Case "WARD", "OUT", "PAT", "LIS"
                        mbAutoLogin = True
                        Me.btnLock.Visible = False

                        sbAutoUpdateLIS()
                End Select
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)

        End Try
    End Sub

    Private Sub sbAutoUpdateDep()
        Dim sFn As String = "Private Sub sbAutoUpdateDep()"

        Try
            Dim sArgs As String = ""

            sArgs += "D" + " "
            sArgs += Convert.ToChar(34) + Application.StartupPath + "\DEP\LIS_DEP.exe" + Convert.ToChar(34) + " "
            sArgs += Convert.ToChar(34) + IIf(USER_INFO.USRID = "", "ACK", USER_INFO.USRID).ToString + Convert.ToChar(34) + " "
            sArgs += Convert.ToChar(34) + My.Computer.Name + "," + Fn.GetIPAddress("") + Convert.ToChar(34)

            Process.Start(Application.StartupPath + "\DEP\LIS_DEP.exe", sArgs)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)

        End Try
    End Sub


    Private Sub sbAutoUpdateLIS()
        Dim sFn As String = "Private Sub sbAutoUpdateLIS()"

        Try
            sbKillPreProc_test()

            Dim sArgs As String = ""

            sArgs += "D" + " "
            sArgs += Convert.ToChar(34) + Application.ExecutablePath + Convert.ToChar(34) + " "
            sArgs += Convert.ToChar(34) + IIf(USER_INFO.USRID = "", "ACK", USER_INFO.USRID).ToString + Convert.ToChar(34) + " "
            sArgs += Convert.ToChar(34) + My.Computer.Name + "," + Fn.GetIPAddress("") + Convert.ToChar(34)

            Process.Start(Application.StartupPath + "\DEP\LIS_DEP.exe", sArgs)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)

        End Try
    End Sub

    Private Sub sbCopyAutoUpdateDep()
        Dim sFn As String = "Private Sub sbCopyAutoUpdateDep()"

        Try
            Dim sArgs As String = ""

            Dim sPath As String = Application.StartupPath + "\DEP\GZIP\LIS_DEP"

            If IO.Directory.Exists(sPath) = False Then Return

            Dim a_sFile As String() = IO.Directory.GetFileSystemEntries(Application.StartupPath + "\DEP\GZIP\LIS_DEP")
            Dim sFileNm As String = ""

            Dim bCopy As Boolean = False

            For i As Integer = 1 To a_sFile.Length
                sFileNm = ""

                If IO.Directory.Exists(a_sFile(i - 1)) Then
                    sFileNm = a_sFile(i - 1).Substring(a_sFile(i - 1).LastIndexOf("\") + 1)

                    For Each sFileSub As String In IO.Directory.GetFiles(a_sFile(i - 1))
                        sFileNm = sFileSub.Replace(sPath, "")

                        fnCopyAutoUpdateDep_Detail(sFileNm)
                    Next
                Else
                    sFileNm = a_sFile(i - 1).Replace(sPath, "")

                    If fnCopyAutoUpdateDep_Detail(sFileNm) And sFileNm.ToLower.EndsWith(".exe") Then
                        bCopy = True
                    End If
                End If
            Next

            If bCopy Then
                For i As Integer = 1 To a_sFile.Length
                    sFileNm = ""

                    If IO.Directory.Exists(a_sFile(i - 1)) Then
                        sFileNm = a_sFile(i - 1).Substring(a_sFile(i - 1).LastIndexOf("\") + 1)

                        For Each sFileSub As String In IO.Directory.GetFiles(a_sFile(i - 1))
                            IO.File.Delete(sFileSub)
                        Next

                        IO.Directory.Delete(a_sFile(i - 1))
                    Else
                        IO.File.Delete(a_sFile(i - 1))
                    End If
                Next

                IO.Directory.Delete(Application.StartupPath + "\DEP\GZIP\LIS_DEP")
            End If

            Return

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)

        End Try
    End Sub

    Private Function fnCopyAutoUpdateDep_Detail(ByVal rsFileNm As String) As Boolean
        Dim sFn As String = "Private Function fnCopyAutoUpdateDep_Detail(String) As Boolean"

        Dim bReturn As Boolean = False

        Dim sPath As String = Application.StartupPath + "\DEP\GZIP\LIS_DEP"

        Try
            IO.File.Copy(sPath + rsFileNm, Application.StartupPath + "\DEP" + rsFileNm, True)

            Return True

        Catch ex As UnauthorizedAccessException
            Dim sFileNmErr As String = Application.StartupPath + "\DEP" + rsFileNm

            Dim iFileAttr As Integer = IO.File.GetAttributes(sFileNmErr)
            Dim a_sFileAttr As String() = IO.File.GetAttributes(sFileNmErr).ToString.Split(CChar(","))

            Dim bReadOnly As Boolean = False

            For j As Integer = 1 To a_sFileAttr.Length
                If a_sFileAttr(j - 1).Trim = IO.FileAttributes.ReadOnly.ToString.Trim Then
                    bReadOnly = True

                    Exit For
                End If
            Next

            If bReadOnly Then
                IO.File.SetAttributes(sFileNmErr, IO.FileAttributes.Normal)

                Try
                    IO.File.Copy(sPath + rsFileNm, sFileNmErr, True)

                Catch e As Exception
                    Fn.log(sFn + " : " + ex.Message)

                End Try

                IO.File.SetAttributes(sFileNmErr, CType(iFileAttr, IO.FileAttributes))

                Return True
            End If

            Return False

        Catch ex As Exception
            Fn.log(sFn + " : " + ex.Message)

            Return False

        End Try
    End Function

    Private Sub sbDeployLIS()
        Dim sFn As String = "Private Sub fnDeployLIS()"

        Try
            Dim sArgs As String = ""

            sArgs += "U" + " "
            sArgs += Convert.ToChar(34) + Application.ExecutablePath + Convert.ToChar(34) + " "
            sArgs += Convert.ToChar(34) + USER_INFO.USRID + Convert.ToChar(34) + " "
            sArgs += Convert.ToChar(34) + My.Computer.Name + "," + Fn.GetIPAddress("") + Convert.ToChar(34)

            Process.Start(Application.StartupPath + "\DEP\LIS_DEP.exe", sArgs)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)

        End Try
    End Sub

    Private Sub sbDisplayMenuMediack()
        Dim sFn As String = "Private Sub sbDisplayMenuMediack()"

        Try
            Dim mmnu As MainMenu

            mmnu = New System.Windows.Forms.MainMenu

            mmnu.MenuItems.Clear()
            mmnu.MenuItems.Add(0, Me.mnuMediack.CloneMenu)

            Me.Menu = mmnu

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)

        End Try
    End Sub

    Private Sub sbDisplayHotList()
        Dim sFn As String = "Private Sub sbDisplayHotList()"

        Try
            Dim alButton As New ArrayList

            alButton.Add(Me.btnHotm00)
            alButton.Add(Me.btnHotm01)
            alButton.Add(Me.btnHotm02)
            alButton.Add(Me.btnHotm03)
            alButton.Add(Me.btnHotm04)
            alButton.Add(Me.btnHotm05)
            alButton.Add(Me.btnHotm06)
            alButton.Add(Me.btnHotm07)
            alButton.Add(Me.btnHotm08)
            alButton.Add(Me.btnHotm09)

            For ix As Integer = 0 To alButton.Count - 1
                CType(alButton.Item(ix), Button).Text = ""
                CType(alButton.Item(ix), Button).Visible = False
            Next

            Dim dt As DataTable = LOGIN.CONFIG.MENU.fnGet_HotListInfo(USER_INFO.USRID)

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                If ix > 9 Then Exit For

                CType(alButton.Item(ix), Button).Text = Space(3) + dt.Rows(ix).Item("mnunm").ToString

                If CInt(dt.Rows(ix).Item("icongbn").ToString) > 0 Then
                    CType(alButton.Item(ix), Button).Image = Me.ImageList.Images(CInt(dt.Rows(ix).Item("icongbn").ToString) - 1)
                End If

                If ix > 0 Then
                    CType(alButton.Item(ix), Button).Left = CType(alButton.Item(ix - 1), Button).Left + CType(alButton.Item(ix - 1), Button).Width + 1
                End If
                CType(alButton.Item(ix), Button).Visible = True
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)

        End Try
    End Sub

    '-- 프로그램 사용 설정
    Private Sub sbGet_PrgInfo()
        Dim sFn As String = "Private Sub sbGet_PrgInfo()"

        Try
            LOGIN.CONFIG.PRGINFO.sbGet_PrgInfo()

            'Dim dt As DataTable = (New WEBSERVER.CGWEB_MAIN).fnGet_PrgInfo()

            'If dt.Rows.Count < 0 Then Return
            'For ix As Integer = 0 To dt.Rows.Count - 1
            '    Select Case dt.Rows(ix).Item("sklcd").ToString
            '        Case "1" : COMMON.CommLogin.PROGRAM.PRGINFO.BCPRTFLG = dt.Rows(ix).Item("sklflg").ToString
            '        Case "2" : COMMON.CommLogin.PROGRAM.PRGINFO.AUTOTKFLG = dt.Rows(ix).Item("sklflg").ToString
            '        Case "3" : COMMON.CommLogin.PROGRAM.PRGINFO.PASSFLG = dt.Rows(ix).Item("sklflg").ToString
            '        Case "4" : COMMON.CommLogin.PROGRAM.PRGINFO.TK2JUBSUFLG = dt.Rows(ix).Item("sklflg").ToString
            '        Case "5" : COMMON.CommLogin.PROGRAM.PRGINFO.RSTMWFLG = dt.Rows(ix).Item("sklflg").ToString
            '        Case "6" : COMMON.CommLogin.PROGRAM.PRGINFO.RSTTNSFLG = dt.Rows(ix).Item("sklflg").ToString
            '    End Select
            'Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)
        End Try

    End Sub

    ' 자동로그인 처리
    Public Function fnLoginDo(ByVal r_al_ParaInfo As ArrayList) As Boolean
        Dim sFn As String = "Public Function fnLoginDo(ArrayList) As Boolean"

        Try
            Dim objIpEntry As IPHostEntry = Dns.GetHostByName(Dns.GetHostName())
            Dim objIpAdrees As IPAddress() = objIpEntry.AddressList

            Dim dt As DataTable = (New WEBSERVER.CGWEB_MAIN).fnGet_UsrInfo(r_al_ParaInfo(0).ToString.Trim)

            If dt.Rows.Count > 0 Then
                With USER_INFO
                    .USRID = dt.Rows(0).Item("usrid").ToString.Trim()
                    .USRNM = dt.Rows(0).Item("usrnm").ToString.Trim()
                    .USRPW = dt.Rows(0).Item("usrpwd").ToString.Trim()
                    .USRLVL = dt.Rows(0).Item("usrlvl").ToString.Trim()
                    .OTHER = dt.Rows(0).Item("other").ToString.Trim()
                    .DRSPYN = dt.Rows(0).Item("drspyn").ToString.Trim()
                    .MEDINO = dt.Rows(0).Item("medino").ToString.Trim()
                    .DELFLG = dt.Rows(0).Item("delflg").ToString.Trim()
                    .USRPW_OLD = dt.Rows(0).Item("pw_old").ToString.Trim()
                    .LOCALIP = objIpAdrees(0).ToString
                End With

                COMMON.CommLogin.LOGIN.USER_SKILL.Clear()
                PRG_CONST.Clear()

                USER_SKILL.SetAuthority = (New WEBSERVER.CGWEB_MAIN).fnGet_UsrSkill(USER_INFO.USRID)    ' 사용자별 사용가능 기능설정
                USER_SKILL.Authority_MST = (New WEBSERVER.CGWEB_MAIN).fnGet_PrgInfo("")                 ' 기능마스터 로드
                COMMON.CommLogin.LOGIN.PRG_CONST.Set_DataTable = (New WEBSERVER.CGWEB_MAIN).fnGet_CONFIG_INFO()

                'Return True

            Else
                USER_INFO.Clear()
                Return False
            End If

            ' 간호사정보 설정
            With USER_INFO
                .N_UID = r_al_ParaInfo(2).ToString.Trim
                .N_UNM = r_al_ParaInfo(3).ToString.Trim

                .N_FLG = r_al_ParaInfo(1).ToString.Trim         '-- 메뉴구분
                .N_WARDorDEPT = r_al_ParaInfo(4).ToString.Trim      '-- WARD:병동코드, OUT:과코드, PAT:등록번호

                '-- 등록번호
                If r_al_ParaInfo.Count > 5 Then
                    .N_REGNO = r_al_ParaInfo(5).ToString.Trim
                End If

                .N_IOGBN = r_al_ParaInfo(0).ToString.Trim
            End With

            Dim sUsrLvl As String = ""

            Select Case r_al_ParaInfo(0).ToString
                Case "WARD"
                    '병동간호사
                    sUsrLvl = "N"

                Case "OUT"
                    '외래간호사
                    sUsrLvl = "R"

                Case "PAT"
                    '진료지원간호사
                    sUsrLvl = "E"
                Case "LIS"
                    sUsrLvl = "1"
            End Select

            Dim sOther As String = ""
            If r_al_ParaInfo.Count > 4 Then sOther = r_al_ParaInfo(4).ToString.Trim


            ' LIS에 존재 유/무에 따라 수정/등록 처리한다.
            If (New WEBSERVER.CGWEB_MAIN).fnExe_UserInfo(r_al_ParaInfo(2).ToString, r_al_ParaInfo(3).ToString, sUsrLvl, sOther) Then fnLoginDo = True


        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        End Try

    End Function

    '> LOGIN01.FGLOGIN01에서 btnLogin.Click Event 처리 함수
    Private Sub sbEventBtnClicked(ByVal rsVal As String, ByVal rsExecGbn As String)
        Dim sFn As String = "Private Sub fnEventBtnClicked(String, String)"

        Me.Enabled = True

        Dim dt As New DataTable
        Dim mmnu As System.Windows.Forms.MainMenu


        Try
            If rsVal.ToUpper = "OK" Then
                Me.btnLogout.Text = "로그아웃"

                dt = LOGIN.CONFIG.MENU.fnGet_MenuInfo(USER_INFO.USRID)
                If dt.Rows.Count > 0 Then
                    Try
                        mmnu = COMMON.Menu.UserDefined.GetMenu(dt, Me.mmnuLIS)

                    Catch ex As Exception
                        MsgBox("사용자별 메뉴구성에 오류가 있습니다. 관리자에게 연락주시기 바랍니다. ", MsgBoxStyle.Information, Me.Text)

                        sbDisplayMenuMediack()

                        Return

                    End Try
                Else
                    sbDisplayMenuMediack()

                    Return

                End If

                '사용자별 설정된 메뉴 표시
                Me.Menu = mmnu

                Me.Text = msTitle + "(" + PRG_CONST.HOSPITAL_NAME + ")" + "@" + (New COMMON.CommDb.Info).GetConnStr.DESCRIPTION

                Select Case rsExecGbn.ToUpper
                    Case "LOGIN"
                        If USER_INFO.USRNM <> "" Then Me.btnLock.Visible = True
                    Case "WARD", "OUT", "PAT"
                        '파라메타정보를 로그인사용자로 설정
                        With USER_INFO
                            .USRID = .N_UID
                            .USRNM = .N_UNM
                            .N_WARDorDEPT = .N_WARDorDEPT
                            .N_IOGBN = rsExecGbn
                        End With

                End Select

                '사용자명 표시
                Me.sbpUser.Text = USER_INFO.USRNM

                '권한 설정
                LISAPP.APP_DB.AuthorityUpdate.setAuthority()

                If mbAutoLogin = False Then Me.tmrAutoUpdate.Enabled = True

            Else
                Me.btnLogout.Text = "로그인"
                Me.Text = msTitle + "(" + PRG_CONST.HOSPITAL_NAME + ")"
                Me.tmrAutoUpdate.Enabled = False

                Me.Close()
            End If

            Me.Activate()

            '< 모든메뉴 ArrayList 만들기
            m_al_mnuAll = New ArrayList

            sbGetMenuAll(Me.Menu.MenuItems, m_al_mnuAll)
            sbDisplayHotList()
            '>

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)

        End Try
    End Sub

    Private Sub sbExecStartMethod()
        Try
            If Me.MdiChildren.Length > 0 Then

                Dim a_objArgs(0) As Object

                a_objArgs(0) = USER_INFO.N_IOGBN

                CallByName(Me.MdiChildren(0), "Set_StartInfo", CallType.Method, a_objArgs)

                If IO.File.Exists(msPathArgsDetail) Then
                    IO.File.Delete(msPathArgsDetail)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sbFormInitialize()
        Dim sFn As String = "Private Sub fnFormInitialize()"


        Try
#If DEBUG Then
            Me.btnTray.Visible = True
#Else
            Try
                Environment.SetEnvironmentVariable("NLS_LANG", "KOREAN_KOREA.KO16MSWIN949", EnvironmentVariableTarget.Machine)

                COMMON.CommXML.setOneElementXML(msXMLDir, msNLS_LANG, "NLS_LANG", "KOREAN_KOREA.KO16MSWIN949")

            Catch ex As Exception

            End Try

            Me.btnTray.Visible = False
#End If

            MdiMain.Frm = Me

            Me.Tag = "Load"
            Me.sbpUser.Text = ""

            Me.tbcMenu.TabPages.Clear()

            '최신 배포파일 내려받기
            sbAutoUpdateDep()

            '자동 Login Info Read
            sbAutoLogin()

            '< MdiForm의 Object를 전역으로 사용하기위해 초기설정
            DS_TabControl.MENU_TABCONTROL = Me.tbcMenu
            DS_StatusBar.MAIN_StatusBar = Me.stbMain
            DS_ProgressBar.MAIN_ProgressBar = Me.pbrMain
            DS_ProgressBar.MAIN_pnlProgress = Me.pnlProgress

            '메뉴초기화
            sbDisplayMenuMediack()
            sbGet_PrgInfo()         '-- 프로그램 사용 설정

            sbSetting_SystemTime()  '-- 서버시간으로 동기화

            Me.Text = msTitle + "(" + PRG_CONST.HOSPITAL_NAME + ")"

            '프로그램 Version 정보 표시
            Me.lblVer.Text = "Ver " + Application.ProductVersion

            '서버시간 설정 Timer 가동
            Me.sbpDateTime.Text = ""
            Me.tmrNowDateTime.Enabled = True

            If mbAutoLogin = False Then
                '자동업데이트 Timer 가동
                Me.tmrAutoUpdate.Enabled = True
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)

            Application.Exit()

        End Try
    End Sub

    Public Sub sbFormLoadedChk(ByVal r_frm As Windows.Forms.Form, ByVal rsFrmText As String)
        Dim sFn As String = "Private Sub sbFormLoadedChk(Form, String)"

        Try
            Select Case rsFrmText
                Case mnuF01.Text, mnuO_base.Text, mnuHelp.Text, mnuF_test.Text, MnuRef.Text, MnuTest2.Text, mnuQ03.Text '<< mnuQ03 - 이미지일괄등록 추가
                    'Owned
                    Me.AddOwnedForm(r_frm)
                    r_frm.WindowState = FormWindowState.Normal

                Case Else
                    'Child
                    If Me.OwnedForms.Length > 0 Then
                        For i As Integer = 0 To Me.OwnedForms.Length - 1
                            If Me.OwnedForms(i).Text.Substring(Me.OwnedForms(i).Text.IndexOf("ː") + 1) = mnuF01.Text Then
                                CType(Me.OwnedForms(i), LISF.FGF01).sbMinimize()
                            End If
                        Next
                    End If

                    If Me.IsMdiContainer Then
                        r_frm.MdiParent = Me
                    Else
                        r_frm.MdiParent = Me.MdiParent
                    End If

            End Select

            r_frm.Text = r_frm.Name + "ː" + rsFrmText

            r_frm.Activate()
            r_frm.Show()

            Select Case rsFrmText
                Case mnuF01.Text, mnuO_base.Text, mnuHelp.Text, mnuF_test.Text, MnuTest2.Text, mnuQ03.Text '<< mnuQ03 - 이미지일괄등록 추가
                Case Else
                    sbTabPageAdd(r_frm)
            End Select

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)

        End Try
    End Sub

    Private Sub sbGetMenuAll(ByVal r_mic As MenuItem.MenuItemCollection, ByVal r_al_mnu As ArrayList)
        Dim sFn As String = "Private Sub sbGetMenuAll(MenuItemCollection, ArrayList)"

        Try
            For Each mnu As MenuItem In r_mic
                If mnu.IsParent Then
                    sbGetMenuAll(mnu.MenuItems, r_al_mnu)
                Else
                    r_al_mnu.Add(mnu)
                End If
            Next

            'sbDisplayHotList()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)

        End Try
    End Sub

    Private Sub sbKillPreProc()
        Dim a_proc As Process() = Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)

        Dim curProc As Process = Process.GetCurrentProcess()

        For i As Integer = a_proc.Length To 1 Step -1
            If curProc.Id <> a_proc(i - 1).Id Then
                a_proc(i - 1).Kill()
            End If
        Next
    End Sub

    Private Sub sbLoadStartForm()
        Select Case USER_INFO.USRLVL
            Case "W"
                If USER_INFO.N_FLG = "C" Then
                    mnuC_Click(Me.mnuC_ward, Nothing)
                ElseIf USER_INFO.N_FLG = "J" Then
                    MenuJ_Click(Me.mnuJ_cancel, Nothing)
                ElseIf USER_INFO.N_FLG = "P" Then
                    MenuJ_Click(Me.mnuJ_bc_reprint, Nothing)
                End If

            Case "O"
                If USER_INFO.N_FLG = "C" Then
                    mnuC_Click(Me.mnuC_out, Nothing)
                ElseIf USER_INFO.N_FLG = "J" Then
                    MenuJ_Click(Me.mnuJ_cancel, Nothing)
                ElseIf USER_INFO.N_FLG = "P" Then
                    MenuJ_Click(Me.mnuJ_bc_reprint, Nothing)
                End If

            Case "P"
                If USER_INFO.N_FLG = "R" Then
                    mnuS_Click(Me.mnuS_rvo, Nothing)
                End If

        End Select
    End Sub

    Private Sub sbReadInfo()
        Dim sFn As String = "Private Sub sbReadInfo()"

        Try
            If IO.File.Exists(msPathArgs) = False Then Return

            Dim sArgs As String = IO.File.ReadAllText(msPathArgs, System.Text.Encoding.Default)

            Dim a_sArgs As String() = sArgs.Split(","c)

            If m_al_Args IsNot Nothing Then
                '> TrayIcon 방식에서 WARD <-> OUT 전환의 경우
                If m_al_Args.Count > 1 And a_sArgs.Length > 1 Then
                    If m_al_Args(0).ToString.Trim() <> a_sArgs(0).Replace(Convert.ToChar(34), "").Trim() Then
                        IO.File.Delete(msPathArgs)

                        Process.Start(Application.ExecutablePath, sArgs.Replace(",", " "))

                        Threading.Thread.Sleep(1000)

                        Process.GetCurrentProcess.Kill()

                        Return
                    End If
                End If
            End If

            'MsgBox(sArgs)

            m_al_Args.Clear()
            m_al_Args.TrimToSize()

            'WARD 작업구분자 사용자ID(사번) 성명 병동코드
            'OUT  작업구분자 사용자ID(사번) 성명 등록번호
            'PAT  작업구분자 사용자ID(사번) 성명 등록번호
            If a_sArgs.Length > 3 Then
                For i As Integer = 1 To a_sArgs.Length
                    m_al_Args.Add(a_sArgs(i - 1).Replace(Convert.ToChar(34), ""))
                Next

                mbAutoLogin = True
            End If

            If mbForceUpdate = False Then
                IO.File.Delete(msPathArgs)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)

        End Try
    End Sub

    Private Sub sbTrayIconOn()
        Dim sFn As String = "Private Sub fnTrayIconOn()"

        Try
            Call LoginPopWin.LogOutDo(Me)

            Me.WindowState = FormWindowState.Minimized

            Me.Hide()

            With Me.niTray
                .Visible = True
                .Text = Me.Text
            End With

            Me.Refresh()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)

        End Try
    End Sub

    Private Sub fnTrayIconOff()
        Me.niTray.Visible = False

        Me.WindowState = FormWindowState.Maximized

        Me.Show()

        Me.Refresh()
    End Sub

    'Private Sub sbWriteInfo()
    '    Dim sFn As String = "Private Sub sbWriteInfo()"

    '    Try
    '        If m_al_Args.Count < 6 Then Return

    '        Dim sw As IO.StreamWriter = IO.File.CreateText(msPathArgs)

    '        Dim sArgs As String = ""

    '        For i As Integer = 1 To m_al_Args.Count
    '            If sArgs.Length > 0 Then sArgs += "|"

    '            sArgs += m_al_Args(i - 1).ToString
    '        Next

    '        sw.Write(sArgs)

    '        sw.Close()

    '    Catch ex As Exception
    '        Fn.log(msFile + sFn, Err)
    '        Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)

    '    End Try
    'End Sub

    Private Sub sbReg_Files()
        Dim sFn As String = "sbReg_Files"

        Dim sFileNm As String = ""

        '> EMRRPT.dll
        Try
            sFileNm = "EMRRPT.dll"

            Dim bReturn As Boolean = IO.File.Exists(Application.StartupPath + "\" + sFileNm)

            If bReturn Then
                Dim objEmrRpt As Object = CreateObject("EMRRPT.CLISEMRRPT")
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, ex.Message + vbCrLf + "Assembly : EMR Report Dll" & "을 등록합니다!!")

            sbReg_Files_Assembly(sFileNm)

        End Try
    End Sub

    Private Sub sbReg_Files_ActiveX(ByVal rsFileNm As String)
        Dim sFn As String = "sbReg_Files_ActiveX"

        Try
            Dim sFileFullPath As String = Windows.Forms.Application.StartupPath + "\" + rsFileNm
            Dim sArg As String = ""
            Dim psi As System.Diagnostics.ProcessStartInfo

            If IO.File.Exists(sFileFullPath) Then
                sArg = "/s " + Convert.ToChar(34) + sFileFullPath + Convert.ToChar(34)
                psi = New System.Diagnostics.ProcessStartInfo("regsvr32.exe", sArg)

                psi.WindowStyle = ProcessWindowStyle.Hidden
                psi.UseShellExecute = False

                Process.Start(psi)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub sbReg_Files_Assembly(ByVal rsFileNm As String)
        Dim sFn As String = "sbReg_Files_Assembly"

        Try
            Dim sFileFullPath As String = Windows.Forms.Application.StartupPath + "\" + rsFileNm
            Dim sArg As String = ""
            Dim psi As System.Diagnostics.ProcessStartInfo

            If IO.File.Exists(sFileFullPath) Then
                sArg = "/codebase /nologo " + Convert.ToChar(34) + sFileFullPath + Convert.ToChar(34)
                psi = New System.Diagnostics.ProcessStartInfo("regasm.exe", sArg)

                psi.WindowStyle = ProcessWindowStyle.Hidden
                psi.UseShellExecute = False

                Process.Start(psi)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

#Region " Menu Event"
    '> 처방관리
    Public Sub mnuO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuO_input.Click, mnuO_srch.Click, mnuO_ord.Click, mnuO_base.Click, mnuO_custlist.Click, mnuO_test.Click, mnuO_Ncov.Click
        Dim mnuItem As MenuItem = CType(sender, MenuItem)
        Dim sFrmTxt As String = mnuItem.Text
        Dim frmChild As Windows.Forms.Form

        frmChild = Ctrl.CheckFormObject(Me, sFrmTxt)

        If frmChild Is Nothing Then
            Select Case mnuItem.Text
                Case mnuO_input.Text
                    '-- 처방입력
                    frmChild = New LISO.FGO01

                Case mnuO_srch.Text
                    '-- 처방내역
                    frmChild = New LISO.FGO02

                Case mnuO_base.Text
                    '-- 수탁관련 기초코드
                    frmChild = New LISO.FGO91
                Case mnuO_ord.Text
                    '-- 수탁처방 입력
                    frmChild = New LISO.FGO03
                Case mnuO_custlist.Text
                    '-- 수탁검사 거래명세서
                    frmChild = New LISO.FGO04
                Case mnuO_test.Text
                    '--테스트
                    frmChild = New LISO.FGO99

#If DEBUG Then
                Case mnuO_Ncov.Text

                    frmChild = New LISO.FGO90
#End If



                Case Else
                    Exit Sub

            End Select
        End If

        sbFormLoadedChk(frmChild, sFrmTxt)
    End Sub

    '> 채혈관리
    Public Sub mnuC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles mnuC_lab.Click, mnuC_ward.Click, mnuC_cust.Click, mnuC_out.Click, mnuC_ward_batch.Click, mnuC_pis_out.Click, mnuC_pis_ward.Click

        Dim mnuItem As MenuItem = CType(sender, MenuItem)
        Dim sFrmTxt As String = mnuItem.Text
        Dim frmChild As Windows.Forms.Form

        frmChild = Ctrl.CheckFormObject(Me, sFrmTxt)

        If frmChild Is Nothing Then
            Select Case mnuItem.Text
                Case mnuC_lab.Text
                    '-- 외래채혈
                    frmChild = New LISC.FGC31()

                Case mnuC_ward.Text
                    '-- 병동채혈
                    If USER_INFO.N_IOGBN = "PAT" Or USER_INFO.N_IOGBN = "WARD" Then
                        frmChild = New LISC.FGC32("I", USER_INFO.N_WARDorDEPT, USER_INFO.N_REGNO)
                    Else
                        frmChild = New LISC.FGC32()
                    End If

                Case mnuC_out.Text
                    '-- 외래간호채혈
                    If USER_INFO.N_WARDorDEPT = "" Then USER_INFO.N_WARDorDEPT = PRG_CONST.DEPT_HC.Item(0).ToString
                    frmChild = New LISC.FGC31("O", USER_INFO.N_WARDorDEPT, USER_INFO.N_REGNO)

                    'Case mnuC_cust.Text
                    '    frmChild = New LISC.FGC03

                Case mnuC_ward_batch.Text
                    If USER_INFO.N_IOGBN = "PAT" Or USER_INFO.N_IOGBN = "WARD" Then
                        frmChild = New LISC.FGC33("I", USER_INFO.N_WARDorDEPT)
                    Else
                        frmChild = New LISC.FGC33("O", USER_INFO.N_WARDorDEPT)
                    End If


                    'Case mnuC_pis_out.Text
                    '    frmChild = New LISC.FGC21

                    'Case mnuC_pis_ward.Text
                    '    frmChild = New LISC.FGC22

                Case Else
                    Exit Sub

            End Select
        End If

        sbFormLoadedChk(frmChild, sFrmTxt)
    End Sub

    '> 접수관리
    Public Sub MenuJ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles mnuJ_take.Click, mnuJ_cancel.Click, mnuJ_bc_reprint.Click, mnuJ_take2.Click, mnuJ_tk_icu.Click, mnuJ_ExLab.Click, mnuJ_pass.Click, mnuJ_PassTake.Click, mnuJ_wl.Click

        Dim mnuItem As MenuItem = CType(sender, MenuItem)
        Dim sFrmTxt As String = mnuItem.Text
        Dim frmChild As Windows.Forms.Form

        frmChild = Ctrl.CheckFormObject(Me, sFrmTxt)

        If frmChild Is Nothing Then
            Select Case mnuItem.Text
                Case mnuJ_take.Text
                    '-- 검체접수
                    frmChild = New LISJ.FGJ01

                Case mnuJ_cancel.Text
                    '-- 채혈/접수 취소
                    frmChild = New LISJ.FGJ02(USER_INFO.N_REGNO)

                Case mnuJ_bc_reprint.Text
                    '-- 바코드 재발행
                    frmChild = New LISJ.FGJ03(USER_INFO.N_REGNO)

                Case mnuJ_take2.Text
                    '-- 부서별 검체접수 
                    frmChild = New LISJ.FGJ04
                Case mnuJ_tk_icu.Text
                    '-- ICU  접수
                    frmChild = New LISJ.FGJ01(PRG_CONST.SLIP_POCT_ICU)

                Case mnuJ_ExLab.Text
                    '-- 위탁검사 리스트 작성
                    frmChild = New LISJ.FGJ06

                Case mnuJ_pass.Text
                    '-- 검체전달
                    frmChild = New LISJ.FGJ07
                Case mnuJ_PassTake.Text
                    '-- 검체전달 및 접수
                    frmChild = New LISJ.FGJ05

                Case mnuJ_wl.Text
                    '-- W/L 생성 및 조회
                    frmChild = New LISJ.FGJ08

                Case Else
                    Exit Sub

            End Select
        End If

        sbFormLoadedChk(frmChild, sFrmTxt)
    End Sub

    '> 결과관리
    Public Sub MenuR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuR_sample.Click, mnuR_labusr.Click, mnuR_item.Click, mnuR_exlab.Click, mnuR_special.Click, mnuR_wl.Click, mnuR_ks.Click, mnuR_icu.Click, mnuR_chg_regno.Click, mnuR_poct.Click, MenuItem1.Click, mnuR_exlab_scl.Click, mnuR_exlab_sml.Click, mnuR_exlab_gcl.Click

        Dim mnuItem As MenuItem = CType(sender, MenuItem)
        Dim sFrmTxt As String = mnuItem.Text
        Dim frmChild As Windows.Forms.Form

        frmChild = Ctrl.CheckFormObject(Me, sFrmTxt)

        If frmChild Is Nothing Then
            Select Case mnuItem.Text
                Case mnuR_sample.Text
                    '-- 검체별 결과저장 및 보고
                    frmChild = New LISR.FGR02

                Case mnuR_labusr.Text
                    '-- 담당자별 결과저장 및 보고
                    frmChild = New LISR.FGR03

                Case mnuR_item.Text
                    '-- 검사항목별 결과저장 및 보고
                    frmChild = New LISR.FGR04

                Case mnuR_exlab.Text
                    '-- 위탁검사 결과저장 및 보고
                    frmChild = New LISR.FGR07

                Case mnuR_exlab_scl.Text
                    '-- 위탁검사 결과저장 및 보고 (SCL)
                    frmChild = New LISR.FGR07_SCL
                Case mnuR_exlab_sml.Text
                    '-- 위탁검사 결과저장 및 보고 (SML)
                    frmChild = New LISR.FGR07_SML
                Case mnuR_exlab_gcl.Text
                    '-- 위탁검사 결과저장 및 보고 (GCL)
                    frmChild = New LISR.FGR07_GCL
                Case mnuR_special.Text
                    '-- 특수검사 결과저장 및 보고
                    frmChild = New LISR.FGR08(2, PRG_CONST.TEST_GV)

                Case mnuR_ks.Text
                    '-- 보관검체 관리
                    frmChild = New LISR.FGR11(False)

                Case mnuR_icu.Text
                    '-- ICU(abga) 결과저장 및 보고
                    frmChild = New LISR.FGR02
                    CType(frmChild, LISR.FGR02).msUse_SlipCd = PRG_CONST.SLIP_POCT_ICU

                Case mnuR_poct.Text
                    '-- 현장검사 결과저장 및 보고
                    frmChild = New LISR.FGR05("O", "", "", "")

                Case mnuR_chg_regno.Text
                    '-- 등록번호 변경
                    frmChild = New LISR.FGR16

                Case mnuR_wl.Text
                    frmChild = New LISR.FGR06

                Case Else
                    Exit Sub

            End Select
        End If

        sbFormLoadedChk(frmChild, sFrmTxt)
    End Sub

    '> 조회
    Public Sub mnuS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuS_abn.Click, mnuS_tat.Click, mnuS_wklist.Click, mnuS_rvo.Click, mnuS_rsheet.Click, mnuS_ordstate.Click, mnuS_rvt.Click, mnuS_stcoll.Click, mnuS_ordhistory.Click, mnuS_clist.Click, mnuS_abn_rst.Click, mnuS_st.Click, mnuS_sttat.Click, mnuS_rv_u.Click, mnuS_rv_a.Click, mnuS_st_fn.Click, mnuS_abn2.Click, mnuS_tk_list.Click, mnuS_coll_st.Click, mnuS_ReTest.Click, mnuS_FnDataCmt.Click, mnuS_tat_tcls.Click, mnuS_st_dr.Click, mnuS_NotColl.Click, mnuS_st_dept.Click, mnuS_Hos.Click, mnuS_st_spc.Click, mnuS_exec.Click, mnuS_Hos2.Click, mnuS_rstcnt.Click

        Dim mnuItem As MenuItem = CType(sender, MenuItem)
        Dim sFrmTxt As String = mnuItem.Text
        Dim frmChild As Windows.Forms.Form

        frmChild = Ctrl.CheckFormObject(Me, sFrmTxt)

        If frmChild Is Nothing Then
            Select Case mnuItem.Text
                Case mnuS_wklist.Text
                    '-- Worklist 조회
                    frmChild = New LISS.FGS13

                Case mnuS_abn_rst.Text
                    ''-- 특이결과 조회
                    frmChild = New LISS.FGS10()

                Case mnuS_abn.Text
                    '-- 이상자 조회
                    frmChild = New LISS.FGS01

                Case mnuS_abn2.Text
                    '-- 이상자2 조회
                    frmChild = New LISS.FGS09

                Case mnuS_tat.Text
                    '-- TurnAroundTime 조회
                    frmChild = New LISS.FGS02

                Case mnuS_sttat.Text
                    '-- TurnAroundTime 통계
                    frmChild = New LIST.FGT03

                Case mnuS_tat_tcls.Text
                    '-- TAT 관리
                    frmChild = New LIST.FGT07

                Case mnuS_rsheet.Text
                    '-- 결과대장
                    frmChild = New LISS.FGS15

                Case mnuS_ordstate.Text
                    '-- 환자/검체현황 조회
                    frmChild = New LISS.FGS04

                Case mnuS_st.Text
                    '-- 검사통계 조회
                    frmChild = New LIST.FGT02

                Case mnuS_st_spc.Text
                    '-- 검체통계 조회
                    frmChild = New LIST.FGT10

                Case mnuS_st_dr.Text
                    '-- 검사통계(처방의) 조회
                    frmChild = New LIST.FGT08

                Case mnuS_st_dept.Text
                    '-- 검사통계(진료과) 조회
                    frmChild = New LIST.FGT09

                Case mnuS_stcoll.Text
                    '-- 채혈통계 조회
                    frmChild = New LISS.FGS05

                Case mnuS_coll_st.Text
                    '-- 채혈통계(시간대별) 조회
                    frmChild = New LIST.FGT06

                Case mnuS_ordhistory.Text
                    '-- 환자/검체이력 조회
                    frmChild = New LISS.FGS06

                Case mnuS_NotColl.Text
                    '-- 미채혈 사유 대장
                    'frmChild = New LISS.FGS08

                Case mnuS_clist.Text
                    '-- 취소내역 조회
                    frmChild = New LISS.FGS07

                Case mnuS_rv_u.Text
                    ''-- 통합 검사결과 조회
                    'frmChild = New R02.FGRV11

                Case mnuS_st_fn.Text
                    '-- 최종보고 수정율 통계 조회
                    frmChild = New LIST.FGT04

                Case mnuS_tk_list.Text
                    '-- 채혈 및 접수대장
                    frmChild = New LISS.FGS03

                Case mnuS_ReTest.Text
                    '-- 재검내역
                    frmChild = New LISS.FGS17

                Case mnuS_Hos.Text
                    '-- 병원체검체결과 신고 
                    frmChild = New LISS.FGS19()

                Case mnuS_Hos2.Text
                    '-- 병원체검체결과 신고 (자동신고)
                    frmChild = New LISS.FGS20()

                Case mnuS_FnDataCmt.Text
                    '-- 최종보고 수정사유 조회
                    frmChild = New LISS.FGS18

                Case mnuS_rvt.Text
                    '-- 결과조회(일일보고서)
                    frmChild = New LISV.FGRV01(True)
                Case mnuS_rvo.Text
                    '-- 결과조회(처방일자별)
                    frmChild = New LISV.FGRV01()

                Case mnuS_rv_a.Text
                    '-- 누적 검사결과 조회
                    frmChild = New LISV.FGRV13(True)

                Case mnuS_exec.Text
                    '-- 실시확인비교
                    frmChild = New LISS.FGS99()
                Case mnuS_rstcnt.Text
                    '--결과값 통계
                    frmChild = New LISS.FGS23

                Case Else
                    Exit Sub

            End Select
        End If

        sbFormLoadedChk(frmChild, sFrmTxt)
    End Sub

    '> 혈액은행
    Public Sub MenuB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuB015.Click, mnuB_tns_jubsu.Click, mnuB_bef_reg.Click, mnuB_bef_cancel.Click, mnuB_out.Click, mnuB_keep.Click, mnuB_abn.Click, mnuB_abn_part.Click, mnuB_bld_history.Click, mnuB_tns_list.Click, mnuB_bld_io_st.Click, mnuB_abn_st.Click, mnuB_abn_list.Click, mnuB047.Click, mnuB048.Click, mnuB049.Click, mnuB_bcno_rst.Click, mnuB_test_rst.Click, mnuB_abo_rst.Click, mnuB_cross_rst.Click, mnuB_aborh_rst.Click, mnuB94.Click, mnuB_outabn_dept.Click, mnuB_XMatch_cnt.Click, mnuB_op.Click, mnuB_aborh_prt.Click, mnuB_IO.Click

        Dim mnuItem As MenuItem = CType(sender, MenuItem)
        Dim sFrmTxt As String = mnuItem.Text
        Dim frmChild As Windows.Forms.Form

        frmChild = Ctrl.CheckFormObject(Me, sFrmTxt)

        If frmChild Is Nothing Then
            Select Case mnuItem.Text

                Case mnuB015.Text
                    '-- 혈액 입고
                    frmChild = New LISB.FGB05

                Case mnuB_tns_jubsu.Text
                    '-- 수혈 의뢰 접수
                    frmChild = New LISB.FGB06

                Case mnuB_bef_reg.Text
                    '-- CrossMatching 등록(가출고)
                    frmChild = New LISB.FGB07
                Case mnuB_bef_cancel.Text
                    '-- 가출고 취소
                    frmChild = New LISB.FGB08

                Case mnuB_out.Text
                    '-- 혈액출고
                    frmChild = New LISB.FGB09

                Case mnuB_keep.Text
                    '-- 보관 검체 관리
                    frmChild = New LISB.FGB12

                Case mnuB_abn.Text
                    '-- 혈액 반납/폐기
                    frmChild = New LISB.FGB10

                Case mnuB_abn_part.Text
                    '-- 혈액 자체 폐기
                    frmChild = New LISB.FGB11

                Case mnuB_bld_history.Text
                    '-- 혈액 이력 조회
                    frmChild = New LISB.FGB13

                Case mnuB_tns_list.Text
                    '-- 수혈의뢰현황 조회
                    frmChild = New LISB.FGB14

                Case mnuB_bld_io_st.Text
                    '--혈액 입고/출고 현황 조회
                    frmChild = New LISB.FGB16

                Case mnuB_abn_st.Text
                    '--혈액 반납/폐기 건수 조회
                    frmChild = New LISB.FGB17

                Case mnuB_abn_list.Text
                    '--혈액 반납/폐기 리스트 조회
                    frmChild = New LISB.FGB15

                    'Case mnuB047.Text
                    '    '--분리혈액 리스트 조회
                    '    frmChild = New LISB.FGB25

                Case mnuB048.Text
                    '--혈액 재고량 조회
                    frmChild = New LISB.FGB18

                Case mnuB049.Text
                    '--혈액 입고/출고 월별 현황 조회
                    frmChild = New LISB.FGB19

                Case mnuB_bcno_rst.Text
                    '-- 검체별 결과저장 및 보고(B)
                    frmChild = New LISR.FGR02(True)
                    frmChild.Text = "검체별 결과저장 및 보고(T)"
                    'CType(frmChild, R01.FGR02).msTitle = "검체별 결과저장 및 보고(T)"
                    'CType(frmChild, R01.FGR02).mbBloodBankYN = True

                Case mnuB_test_rst.Text
                    '-- 검사항목별 결과저장 및 보고(B)
                    frmChild = New LISR.FGR04(True)
                    'frmChild.Text = "검사항목별 결과저장 및 보고(T)"
                    CType(frmChild, LISR.FGR04).msTitle = "검사항목별 결과저장 및 보고(T)"

                Case mnuB_abo_rst.Text
                    '-- 혈액형 결과수정 및 보고(B)
                    frmChild = New LISR.FGR02(True)
                    frmChild.Text = "검체별 결과저장 및 보고(T)"
                    'CType(frmChild, R01.FGR02).msTitle = "검체별 결과저장 및 보고(T)"
                    'CType(frmChild, R01.FGR02).mbBloodBankYN = True
                    CType(frmChild, LISR.FGR02).mbBloodBankModify = True

                Case mnuB_cross_rst.Text
                    '-- Cross Matching 결과 수정
                    frmChild = New LISB.FGB21

                Case mnuB_aborh_rst.Text
                    '-- 혈액형 2차결과 등록
                    frmChild = New LISB.FGB04

                Case mnuB94.Text
                    '-- 혈액반납폐기율
                    frmChild = New LISB.FGB23

                Case mnuB_outabn_dept.Text
                    '-- 진료과별 출고/폐기 현황
                    frmChild = New LISB.FGB20

                Case mnuB_XMatch_cnt.Text
                    '-- 진료과별 X-Matching 현황
                    frmChild = New LISB.FGB22

                Case mnuB_aborh_prt.Text
                    '-- 혈액형 결과대장
                    frmChild = New LISB.FGB25

                Case mnuB_op.Text
                    '-- 수술환자 확정 조회
                    frmChild = New LISB.FGB24

                    'Case mnuB_don_jubsu.Text
                    '    '-- 헌혈 접수 및 판정
                    '    frmChild = New LISB.FGB02

                    'Case mnuB_don_exec.Text
                    '    '-- 헌혈 시행 및 혈액번호 발생
                    '    frmChild = New LISB.FGB03

                Case mnuB_IO.Text
                    '-- 질병관리본부 입출고 조회
                    frmChild = New LISB.FGB26

                Case Else
                    Exit Sub

            End Select
        End If

        sbFormLoadedChk(frmChild, sFrmTxt)
    End Sub

    '> 미생물
    Public Sub mnuM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuM_sample.Click, mnuM_item.Click, mnuM_ng.Click, mnuM_gr.Click, mnuM_ks.Click, mnuM_statistics.Click, mnuM_abn.Click, mnuM_wl_exe.Click, mnuM_wl.Click, mnuM_rstsheet.Click

        Dim mnuItem As MenuItem = CType(sender, MenuItem)
        Dim sFrmTxt As String = mnuItem.Text
        Dim frmChild As Windows.Forms.Form

        frmChild = Ctrl.CheckFormObject(Me, sFrmTxt)

        If frmChild Is Nothing Then
            Select Case mnuItem.Text

                Case mnuM_sample.Text
                    '-- 검사분야별 결과저장 및 보고
                    frmChild = New LISM.FGM01

                Case mnuM_item.Text
                    '-- 검사항목별 결과저장 및 보고
                    frmChild = New LISM.FGM02

                Case mnuM_ng.Text
                    '-- No growth 결과저장 및 보고
                    frmChild = New LISM.FGM03

                Case mnuM_gr.Text
                    '-- 양성자 조회
                    frmChild = New LISM.FGM11

                Case mnuM_ks.Text
                    ''-- 보관 검체 관리 (M)
                    frmChild = New LISR.FGR11(True)

                Case mnuM_statistics.Text
                    '-- 미생물 통계
                    frmChild = New LIST.FGT05
                Case mnuM_abn.Text
                    '-- 미생물 검사결과 조회
                    frmChild = New LISS.FGS09(True)

                Case mnuM_wl.Text
                    '-- Worklist 조회 및 인쇄
                    frmChild = New LISS.FGS13(True)

                Case mnuM_rstsheet.Text
                    '-- 결과대장
                    frmChild = New LISS.FGS15(True)
                Case mnuM_wl_exe.Text
                    frmChild = New LISJ.FGJ08(True)

                Case Else
                    Exit Sub

            End Select
        End If

        sbFormLoadedChk(frmChild, sFrmTxt)
    End Sub

    '> 검사실인증
    Public Sub mnuQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuQ01.Click, mnuQ02.Click, mnuQ03.Click
        Dim mnuItem As MenuItem = CType(sender, MenuItem)
        Dim sFrmTxt As String = mnuItem.Text
        Dim frmChild As Windows.Forms.Form

        frmChild = Ctrl.CheckFormObject(Me, sFrmTxt)

        If frmChild Is Nothing Then
            Select Case mnuItem.Text
                Case mnuQ01.Text
                    '정도관리(QC)
                    Dim a_ps() As Diagnostics.Process = Diagnostics.Process.GetProcessesByName("SHBC_QC")
                    Dim sArgu As String = ""

                    If a_ps.Length > 0 Then
                        Microsoft.VisualBasic.AppActivate(a_ps(0).Id)
                    Else
                        sArgu = "C:\Program Files\ACK@QC\" + "/" + USER_INFO.USRID + "/" + USER_INFO.USRNM + "/" + USER_INFO.USRPW + "/" + USER_INFO.USRLVL

                        If IO.File.Exists("C:\Program Files\ACK@QC\ACK@QCv2.exe") Then
                            Process.Start("C:\Program Files\ACK@QC\ACK@QCv2.exe", sArgu)
                        End If
                    End If

                    Return

                Case mnuQ02.Text
                    '-- 특수검사 결과저장 및 보고
                    '      종합검증만 포함
                    frmChild = New LISR.FGR09(1, PRG_CONST.TEST_GV)
                    CType(frmChild, LISR.FGR09).msUse_PartCd = PRG_CONST.PART_GeneralVerify

                    frmChild.WindowState = Windows.Forms.FormWindowState.Maximized

                Case mnuQ03.Text
                    '이미지 일괄등록
                    frmChild = New LISR.FGR08_S04

                Case Else
                    Exit Sub
            End Select
        End If

        sbFormLoadedChk(frmChild, sFrmTxt)
    End Sub

    '> 기초마스터
    Public Sub mnuF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuF01.Click, mnuHelp.Click, mnuF_test.Click, MnuRef.Click, MnuTest2.Click
        Dim mnuItem As MenuItem = CType(sender, MenuItem)
        Dim sFrmTxt As String = mnuItem.Text
        Dim frmChild As Windows.Forms.Form

        frmChild = Ctrl.CheckFormObject(Me, sFrmTxt)

        If frmChild Is Nothing Then
            Select Case mnuItem.Text
                Case mnuF01.Text
                    '-- 기초마스터 관리
                    frmChild = New LISF.FGF01
                Case mnuF_test.Text
                    '-- 검사코드 
                    frmChild = New LISF.FGF11

                Case mnuHelp.Text
                    '-- LIS 검사정보
                    'frmChild = New CDHELP.FGCDHELP_TEST
                    frmChild = New CDHELP.FGCDHELP_TEST_NEW

                Case MnuRef.Text
                    '-- 병원체 코드 관리 
                    frmChild = New LISF.FGF21

                Case MnuTest2.Text
                    frmChild = New CDHELP.FGCDHELP_TEST_NEW

                Case Else
                    Exit Sub

            End Select
        End If

        sbFormLoadedChk(frmChild, sFrmTxt)
    End Sub

    '> MDI 창
    Public Sub MnuWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles mnuCascade.Click, mnuTileHorizontal.Click, mnuTileVertical.Click

        Dim mnuItem As MenuItem = CType(sender, MenuItem)

        If mnuCascade.Text.Equals(mnuItem.Text) Then
            '-- 계단식 정렬
            Me.LayoutMdi(MdiLayout.Cascade)

        ElseIf mnuTileHorizontal.Text.Equals(mnuItem.Text) Then
            '-- 수평 바둑판식 정렬
            Me.LayoutMdi(MdiLayout.TileHorizontal)

        ElseIf mnuTileVertical.Text.Equals(mnuItem.Text) Then
            '-- 수직 바둑판식 정렬
            Me.LayoutMdi(MdiLayout.TileVertical)

        End If

    End Sub

    '> MEDI@CK 정보
    Public Sub mnuAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAbout.Click
        Dim about As New ABOUT

        about.ShowDialog()
    End Sub

#End Region

    '> Control Event : MAIN
    Private Sub MAIN_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Dim sFn As String = "MyBase.Activated"

        Try
            If Me.Tag.ToString = "ACTIVATED" Then
                sbReadInfo()

                If m_al_Args.Count > 1 Then
                    If m_al_Args(1).ToString.Trim = "E" Then
                        End
                    End If
                End If

                Return
            End If

            Me.TopMost = True

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text + "-" + sFn)

            'OCS와 자동연동일때 에러는 종료
            If mbAutoLogin Then
                Application.Exit()
            End If

        Finally
            Me.Tag = "ACTIVATED"
            Me.TopMost = False

        End Try
    End Sub

    Private Sub MAIN_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If mbAutoLogin Then
            e.Cancel = False
        Else
            If MsgBox("프로그램을 종료하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                Application.Exit()
            Else
                e.Cancel = True

            End If
        End If
    End Sub

    Private Sub MAIN_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        DbClose()
    End Sub


    Private Sub MAIN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
#If DEBUG Then
        If USER_INFO.USRLVL = "S" Then
            Dim sFile As String = ""
            Dim sDir As String = ""

            If e.Control Then
                Select Case e.KeyCode

                    'Case Keys.F10
                    '    fnDeployLIS()

                    '    Return

                    Case Keys.F11
                        sDir = Application.StartupPath + "\ErrLog"
                        sFile = sDir + "\Err" + Format(Now, "yyyy-MM-dd") + ".txt"

                    Case Keys.F12
                        sDir = Application.StartupPath + "\SqlLog"
                        sFile = sDir + "\SQL" + Format(Now, "yyyy-MM-dd") + ".txt"

                    Case Else
                        Return

                End Select

                Process.Start(sFile)

            End If
        End If
#Else
        If USER_INFO.USRLVL = "S" And (USER_INFO.USRID.ToUpper.StartsWith("ACK") Or USER_INFO.USRID.ToUpper.StartsWith("IT")) Then
            If e.Control = True And e.KeyCode = Keys.F10 Then
                sbDeployLIS()
            End If
        End If
#End If

        If e.KeyCode = Keys.F1 Then
            mnuF_Click(mnuHelp, Nothing)
        End If

        Dim frmChild As Windows.Forms.Form = Nothing
        Dim sFrmTxt As String = ""

        If e.Control Then
            Select Case e.KeyCode
                Case Keys.S
                    frmChild = New LISC.FGC31

                    sFrmTxt = "외래채혈"
                Case Keys.R
                    frmChild = New LISJ.FGJ01

                    sFrmTxt = "검체접수"

                Case Keys.P
                    frmChild = New LISJ.FGJ03

                    sFrmTxt = "바코드 재출력"

                Case Keys.T
                    frmChild = New LISR.FGR02

                    sFrmTxt = "검체별 결과저장 및 보고"

                Case Keys.M
                    frmChild = New LISR.FGR03

                    sFrmTxt = "담당자별 결과저장 및 보고"

                Case Keys.I
                    frmChild = New LISV.FGRV01

                    sFrmTxt = "결과조회(처방일자별)"

                Case Keys.H
                    frmChild = New LISS.FGS06

                    sFrmTxt = "환자/검체 History 조회"
            End Select

            If Not frmChild Is Nothing And sFrmTxt <> "" Then
                sbFormLoadedChk(frmChild, sFrmTxt)
            End If
        End If

    End Sub

    Private Sub MAIN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        sbReg_Files_ActiveX("richtx32.ocx")
        sbReg_Files_ActiveX("TeeChart7.ocx")
        sbReg_Files_ActiveX("MSCOMM32.OCX")

        sbKillPreProc_old()

        Try
            If IO.File.Exists("C:\Program Files\ACK@LIS\ACK@LIS.exe") Then
                FileCopy("C:\Program Files\ACK@LIS\ACK@LIS.exe", "C:\Program Files\ACK@LIS\ACK@LIS_old.exe")

            End If

        Catch ex As Exception

        End Try

        Try
            If IO.File.Exists("C:\Program Files\ACK@LIS\ACK@LIS.exe") Then
                IO.File.Delete("C:\Program Files\ACK@LIS\ACK@LIS.exe")
            End If

        Catch ex As Exception

        End Try


        If IO.File.Exists(Windows.Forms.Application.StartupPath + "\stdole.dll") Then
        Else
            Try
                IO.File.Copy(Windows.Forms.Application.StartupPath + "\SSF\stdole.dl_", Windows.Forms.Application.StartupPath + "\stdole.dll")
            Catch ex As Exception

            End Try
        End If

        If IO.File.Exists(Windows.Forms.Application.StartupPath + "\common.dll") Then
            Try
                IO.File.Copy(Windows.Forms.Application.StartupPath + "\common.dll", "C:\Program Files\EMR_UREF\common.dll")
            Catch ex As Exception

            End Try
        End If

        'If IO.File.Exists("C:\WINDOWS\system32\VB6KO.DLL") Then
        'Else
        '    Try
        '        IO.File.Copy(Windows.Forms.Application.StartupPath + "\VB6KO.DLL", "C:\WINDOWS\system32\VB6KO.DLL")
        '    Catch ex As Exception

        '    End Try
        'End If

    End Sub

    Private Sub MAIN_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Dim sFn As String = "Private Sub MAIN_Resize(\Object, EventArgs) Handles MyBase.Resize"

        Try
            If Me.WindowState = FormWindowState.Minimized Then
                If Me.OwnedForms.Length > 0 Then
                    For i As Integer = 0 To Me.OwnedForms.Length - 1
                        Dim sFormNm As String = Me.OwnedForms(i).Text
                        If sFormNm.IndexOf("ː") > 0 Then sFormNm = sFormNm.Substring(sFormNm.IndexOf("ː") + 1)
                        If sFormNm = mnuF01.Text Then
                            CType(Me.OwnedForms(0), LISF.FGF01).sbMinimize()
                            Exit Sub
                        ElseIf sFormNm = mnuF_test.Text Then
                            CType(Me.OwnedForms(0), LISF.FGF11).sbMinimize()
                            Exit Sub
                        End If
                    Next
                End If

                Return
            End If

            If Me.WindowState = FormWindowState.Maximized Or Me.WindowState = FormWindowState.Normal Then
                If Me.OwnedForms.Length > 0 Then
                    For i As Integer = 0 To Me.OwnedForms.Length - 1
                        Dim sFormNm As String = Me.OwnedForms(i).Text
                        If sFormNm.IndexOf("ː") > 0 Then sFormNm = sFormNm.Substring(sFormNm.IndexOf("ː") + 1)

                        If sFormNm = mnuF01.Text Then
                            CType(Me.OwnedForms(0), LISF.FGF01).sbRestore()
                        ElseIf sFormNm = "Lock" Then
                            'Lock폼 표시때 Main화면 Enabled=False
                            Me.Enabled = False
                        End If
                    Next
                End If

                Return
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub


    Private Sub MAIN_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.TopMost = False

        If mbAutoLogin = True Then
            sbKillPreProc()
            '자동로그인 처리

            If IO.File.Exists(msPathArgsDetail) Then
                m_al_Args.Clear()

                Dim sArgs As String = ""
                Dim a_sArgs() As String

                sArgs = IO.File.ReadAllText(msPathArgsDetail, System.Text.Encoding.Default)
                a_sArgs = sArgs.Split

                For i As Integer = 0 To a_sArgs.Length - 3
                    m_al_Args.Add(a_sArgs(i).Replace(",", ""))
                Next
            End If

            If fnLoginDo(m_al_Args) Then
                sbEventBtnClicked("OK", m_al_Args(0).ToString)
            End If

            'If LoginPopWin.LogInDo(m_al_Args) Then
            '    sbEventBtnClicked("OK", m_al_Args(0).ToString)
            'Else
            '    'If m_al_Args(2).ToString <> "E" Then
            '    '    MsgBox("OCS와 연동 오류로 화면을 표시할수 없습니다.", MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, Me.Text)
            '    'End If
            'End If

            sbLoadStartForm()
            sbExecStartMethod()

            If m_al_Args.Count > 1 Then
                If m_al_Args(1).ToString = "E" Then End
            End If
        Else
#If DEBUG Then
#Else
            sbAutoUpdateLIS()
#End If
            sbChange_srv()

            '일반사용자 로그인
            btnLogout_Click(Nothing, Nothing)
        End If

        Try

            Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXMLDir, msAutoLogout, "AUTOLOGOUT")
            If sTmp = "1" Then Me.btnAutoLogon.Text = Me.btnAutoLogon.Text.Replace("OFF", "ON")

            If IO.File.Exists(msPathArgsDetail) Then
                IO.File.Delete(msPathArgsDetail)
            End If
        Catch ex As Exception

        End Try

    End Sub

    '> Control Event : Timer
    Private Sub tmrNowDateTime_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrNowDateTime.Tick
        Dim sFn As String = "Handles tmrNowDateTime.Tick"

        'Static iSec As Integer = 0
        Static dtSvrDateTime As Date = Now

        Try
            dtSvrDateTime = Now

            'If iSec > 3600 Then
            '    iSec = 0

            'ElseIf iSec = 0 Then
            '    iSec += 1
            '    'dtSvrDateTime = (New ServerDateTime).GetDateTimeWithNewCn
            '    dtSvrDateTime = Now
            'Else
            '    iSec += 1
            '    dtSvrDateTime = dtSvrDateTime.AddSeconds(1)

            'End If

            sbpDateTime.Text = dtSvrDateTime.ToString("yyyy-MM-dd HH:mm:ss")
            MainServerDateTime.mServerDateTime = dtSvrDateTime

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
        End Try
    End Sub

    Private Sub tmrAutoUpdate_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrAutoUpdate.Tick
        Dim sFn As String = "tmrAutoUpdate.Tick"

        If mbAutoLogin Then Return

        Static iTime As Integer = 0
        Dim rbDbCloseYn As Boolean = False

        Try
            Me.tmrAutoUpdate.Enabled = False
            Dim sDbConnect As String = COMMON.CommFN.MdiMain.Db_ConnectTimeOut '분 (30분 설정되어있음) 

            If sDbConnect <> "" And Me.btnAutoLogon.Text.IndexOf("ON") >= 0 Then
                iTime += Me.tmrAutoUpdate.Interval

                If (iTime / 1000) >= Convert.ToInt32(sDbConnect) * 60 And COMMON.CommFN.MdiMain.DB_Active_YN = "" Then
                    rbDbCloseYn = True
                    iTime = 0
                ElseIf COMMON.CommFN.MdiMain.DB_Active_YN = "Y" Then
                    iTime = 0
                End If
            End If

            If rbDbCloseYn Then

                Dim invas_buf As New InvAs
                Dim sReturn As String

                With invas_buf
                    .LoadAssembly(Windows.Forms.Application.StartupPath + "\CDHELP.DLL", "CDHELP.FGMSGDELAY")

                    Dim a_objParam() As Object
                    ReDim a_objParam(0)

                    a_objParam(0) = Me

                    sReturn = CType(.InvokeMember("Display_Result", a_objParam), String)

                    If sReturn Is Nothing Then Return
                End With

                If sReturn = "OK" Then
                    btnLogout_Click(Nothing, Nothing)
                    Return
                Else
                    rbDbCloseYn = False
                End If
            End If

            Dim sFileVer As String = Diagnostics.FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion

            Dim sPrgFileNm As String = Application.ExecutablePath.Substring(Application.ExecutablePath.LastIndexOf("\") + 1)
            Dim sPrgId As String = sPrgFileNm.Substring(0, sPrgFileNm.LastIndexOf("."))

            Dim sDepDt As String = LISAPP.APP_DEP.Find_DepFile_NewVersion(sPrgId, sPrgFileNm, sFileVer)
            'Dim sDepDt As String = (New WEBSERVER.CGWEB_MAIN).fnGet_DepFile_NewVersion(sPrgId, sPrgFileNm, sFileVer)

            If IO.File.Exists(Application.StartupPath + "\" + "LIS_Update_Compulsory.txt") Then
                '> 배포 테스트의 경우 사용 
                sDepDt = LISAPP.APP_DEP.Find_DepFile_NewVersion_DepTest(sPrgId, sPrgFileNm, sFileVer)

                'sDepDt = (New WEBSERVER.CGWEB_MAIN).fnGet_DepFile_NewVersion(sPrgId, sPrgFileNm, sFileVer)

            End If

            If IsDate(sDepDt) = False Then
                sbCopyAutoUpdateDep()
                Return
            End If

#If DEBUG Then
            If IO.File.Exists(Application.StartupPath + "\" + "LIS_Update_Compulsory.txt") = False Then
                Return
            End If
#End If

            If MsgBox("확인을 누르시면 최신 버전으로 업그레이드 됩니다.", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.OkCancel, "업그레이드") = MsgBoxResult.Cancel Then
                Return
            End If

            'sbWriteInfo()

            mbForceUpdate = True

            sbAutoUpdateLIS()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

        Finally
            If rbDbCloseYn = False And mbAutoLogin = False Then Me.tmrAutoUpdate.Enabled = True

        End Try
    End Sub

    '> Control Event : Login/Logout/Lock/Exit
    Private Sub btnLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogout.Click

        Dim sFn As String = "Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click"

        Dim LoginPopWin As New LoginPopWin
        Dim objMenuView As New System.Windows.Forms.MainMenu

        Try
            Me.tmrAutoUpdate.Enabled = False

            '메뉴 MEDI@CK만 표시
            objMenuView = New System.Windows.Forms.MainMenu

            objMenuView.MenuItems.Add(0, Me.mnuMediack.CloneMenu)
            Me.Menu = objMenuView

            Me.btnLock.Visible = False

            USER_INFO.Clear()
            USER_SKILL.Clear()

            '열려있는 폼 모두 종료
            Call LoginPopWin.LogOutDo(Me)

            Call LoginPopWin.LogInDo(Me)

            AddHandler LoginPopWin.EventBtnClicked, AddressOf sbEventBtnClicked

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)
        End Try

    End Sub

    '> Control Event : Logo
    Private Sub picLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picLogo.Click
        'ACK 원격지원사이트 열기
        Process.Start("http://ack.anyhelp.net")
    End Sub

    Private Sub picLogo_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles picLogo.MouseEnter
        Me.Cursor = Cursors.Hand

        m_tooltip = New ToolTip

        m_tooltip.SetToolTip(picLogo, "ACK 원격지원 사이트 열기")
        m_tooltip.Active = True
    End Sub

    Private Sub picLogo_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles picLogo.MouseLeave
        Me.Cursor = Cursors.Default

        If m_tooltip Is Nothing Then Return

        m_tooltip.Active = False

        m_tooltip.Dispose()
    End Sub

    '> Control Event : TrayIcon
    Private Sub btnTray_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTray.Click
        sbTrayIconOn()
    End Sub

    Private Sub niTray_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles niTray.DoubleClick
        fnTrayIconOff()
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click

        Me.Close()

    End Sub

    Private Sub tbcMenu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbcMenu.SelectedIndexChanged
        Dim sFn As String = "tbcMenu_SelectedIndexChanged"

        Try
            If tbcMenu.TabCount < 1 Then Return
            Me.Cursor = Cursors.WaitCursor

            For ix As Integer = 0 To m_al_mnuAll.Count - 1
                Dim mnuBuf As MenuItem = CType(m_al_mnuAll(ix), MenuItem)

                If mnuBuf.Text.Substring(mnuBuf.Text.IndexOf("ː") + 1) = tbcMenu.TabPages(tbcMenu.SelectedIndex).Text Then
                    mnuBuf.PerformClick()
                    Exit For
                End If
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub btnHotList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHotList.Click

        Dim frmChild As Windows.Forms.Form

        frmChild = Ctrl.CheckFormObject(Me, "즐겨찾기 편집")

        If frmChild Is Nothing Then
            frmChild = New HOTLIST

            Me.AddOwnedForm(frmChild)
            frmChild.WindowState = FormWindowState.Normal

            frmChild.MdiParent = Me

            frmChild.Activate()
            frmChild.Show()

            sbDisplayHotList()
        End If

    End Sub

    Private Sub btnTabClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTabClose.Click

        Dim sFn As String = "btnTabClose_Click"

        Try
            If Me.tbcMenu.TabPages.Count < 1 Then Return

            Me.Cursor = Cursors.WaitCursor

            If Me.tbcMenu.TabPages(Me.tbcMenu.SelectedIndex).Text = "기초마스터 관리" Then
                For ix As Integer = 1 To Me.OwnedForms.Length
                    Dim sFormNm As String = Me.OwnedForms(ix - 1).Text
                    If sFormNm.IndexOf("ː") > 0 Then sFormNm = sFormNm.Substring(sFormNm.IndexOf("ː") + 1)

                    If sFormNm = "기초마스터 관리" Then
                        Me.OwnedForms(ix - 1).Close()
                        Return
                    End If
                Next

            Else

                Dim intMdiChildrenCnt As Integer = Me.MdiChildren.Length
                For ix As Integer = 1 To intMdiChildrenCnt
                    Dim sFormNm As String = Me.MdiChildren(ix - 1).Text
                    If sFormNm.IndexOf("ː") > 0 Then sFormNm = sFormNm.Substring(sFormNm.IndexOf("ː") + 1)

                    If Me.tbcMenu.TabPages(Me.tbcMenu.SelectedIndex).Text = sFormNm Then
                        Me.MdiChildren(ix - 1).Close()

                        Return
                    End If
                Next
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        Finally
            Me.Cursor = Cursors.Default

        End Try

    End Sub

    Private Sub btnHotm00_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHotm00.Click, btnHotm01.Click, btnHotm02.Click, btnHotm03.Click, btnHotm04.Click, btnHotm05.Click, btnHotm06.Click, btnHotm07.Click, btnHotm08.Click, btnHotm09.Click
        Dim sFn As String = "btnHotm00_Click"

        Try
            Me.Cursor = Cursors.WaitCursor

            For ix As Integer = 0 To m_al_mnuAll.Count - 1
                Dim mnuBuf As MenuItem = CType(m_al_mnuAll(ix), MenuItem)

                If mnuBuf.Text = CType(sender, Button).Text.Trim Then
                    mnuBuf.PerformClick()
                    Exit For
                End If
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnLock_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLock.Click
        Dim sFn As String = ""

        Dim LoginPopWin As New LoginPopWin
        Try
            Call LoginPopWin.Locking(Me)
            AddHandler LoginPopWin.EventBtnClicked, AddressOf sbEventBtnClicked

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub btnAutoLogon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAutoLogon.Click

        If Me.btnAutoLogon.Text.IndexOf("OFF") >= 0 Then
            Me.btnAutoLogon.Text = Me.btnAutoLogon.Text.Replace("OFF", "ON")
        Else
            Me.btnAutoLogon.Text = Me.btnAutoLogon.Text.Replace("ON", "OFF")
        End If

        COMMON.CommXML.setOneElementXML(msXMLDir, msAutoLogout, "AUTOLOGOUT", IIf(Me.btnAutoLogon.Text.IndexOf("ON") >= 0, "1", "0").ToString)

    End Sub

    Private Sub mnuSet_EmrPrt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSet_EmrPrt.Click
        sbSetting_EmrPrint()
    End Sub

    Private Sub mnuAS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAS.Click

        Try
            'AS 접수 및 현황
            Dim a_ps() As Diagnostics.Process = Diagnostics.Process.GetProcessesByName("ACK@AS")
            Dim sArgu As String = ""
            Dim sFile As String = Application.StartupPath + "\ACK@AS\ACK@AS.exe"

            If a_ps.Length > 0 Then
                Microsoft.VisualBasic.AppActivate(a_ps(0).Id)
            Else
                sArgu = "LIS" + " " + PRG_CONST.HOSPITAL_CODE + " " + USER_INFO.USRID + " " + USER_INFO.OTHER

                If IO.File.Exists(sFile) Then
                    Process.Start(sFile, sArgu)
                End If
            End If
        Catch ex As Exception

        End Try
 
    End Sub
End Class
