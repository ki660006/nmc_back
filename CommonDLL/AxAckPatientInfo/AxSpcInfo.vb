Imports COMMON.CommFN
Imports System.Windows.Forms

Public Class AxSpcInfo
    Private Const mc_sFile As String = "File : AxSpcInfo.vb, Class : AxSpcInfo" & vbTab

    Public ReadOnly Property RegNo() As String
        Get
            Return Me.lblRegNo.Text
        End Get
    End Property

    Public ReadOnly Property PatNm() As String
        Get
            Return Me.lblPatNm.Text
        End Get
    End Property

    Public ReadOnly Property SexAge() As String
        Get
            Return Me.lblSexAge.Text
        End Get
    End Property


    Public Sub sbDisplay_SpcInfo(ByVal r_si As AxAckResultViewer.SpecimenInfo)
        Dim sFn As String = "sbDisplay_SpcInfo"

        Try
            Dim iCol As Integer = 0
            Dim iRow As Integer = 0

            Me.lblRegNo.Text = r_si.RegNo.ToString() ' 등록번호 
            Me.lblPatNm.Text = r_si.PatNm.ToString() ' 환자명 
            Me.lblSexAge.Text = r_si.SexAge.ToString() ' 성별/나이  
            Me.txtDiagNm.Text = r_si.DiagNm.ToString() ' 진단명  
            Me.txtRemark.Text = r_si.Remark.ToString() ' 의사 리마크   

            Me.lblHeight.Text = r_si.Height
            Me.lblWeight.Text = r_si.Weight
            Me.lblInjong.Text = r_si.Injong
            Me.lblAbo.Text = OCSAPP.OcsLink.Pat.fnGet_Pat_AboRh(r_si.RegNo)

            Me.lblOrdDt.Text = r_si.OrdDt.ToString() ' 처방일시
            Me.lblDeptCd.Text = r_si.DeptNm.ToString() ' 진료과  
            Me.lblDoctorNm.Text = r_si.DoctorNm.ToString() ' 진료의  
            Me.lblEntDt.Text = r_si.EntDt.ToString() ' 입원일시  
            Me.lblWardRoom.Text = r_si.WardRoom.ToString() ' 병동병실  

            Me.lblCollDt.Text = r_si.CollDt.ToString()  ' 채혈일시  
            Me.lblCollNm.Text = r_si.CollUsr.ToString() ' 채혈자  
            Me.lblTkDt.Text = r_si.TkDt.ToString()      ' 접수일시  
            Me.lblTkNm.Text = r_si.TkUsr.ToString()     ' 접수자  

            Me.lblFnDt.Text = r_si.RegDt.ToString     ' 검사일시  
            Me.lblFnNm.Text = r_si.RegUsr.ToString    ' 검사자  
            Me.lblCfDt.Text = r_si.FnDt.ToString()    ' 보고일시  
            Me.lblCfNm.Text = r_si.TestUsr.ToString    ' 확인자  

            'Me.lblFnDt.Text = r_si.TestDt.ToString()    ' 검사일시  
            'Me.lblFnNm.Text = r_si.TestUsr.ToString()   ' 검사자  
            'Me.lblCfDt.Text = r_si.FnDt.ToString()      ' 보고일시  
            'Me.lblCfNm.Text = r_si.LabDrNm.ToString()   ' 확인자  

            Me.lblInfInfo.Text = r_si.InfInfo.Trim()    '-- 감염정보

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        sbInit()
    End Sub

    Public Sub sbInit()
        Dim sFn As String = "sbInit"

        Try
            Me.lblRegNo.Text = ""
            Me.lblPatNm.Text = ""
            Me.lblSexAge.Text = ""
            Me.txtDiagNm.Text = ""
            Me.txtRemark.Text = ""

            Me.lblHeight.Text = ""
            Me.lblWeight.Text = ""
            Me.lblInjong.Text = ""
            Me.lblAbo.Text = ""

            Me.lblOrdDt.Text = ""
            Me.lblDeptCd.Text = ""
            Me.lblDoctorNm.Text = ""
            Me.lblEntDt.Text = ""
            Me.lblWardRoom.Text = ""


            Me.lblCollDt.Text = ""
            Me.lblCollNm.Text = ""
            Me.lblTkDt.Text = ""
            Me.lblTkNm.Text = ""
            Me.lblFnDt.Text = ""
            Me.lblFnNm.Text = ""
            Me.lblCfDt.Text = ""
            Me.lblCfNm.Text = ""

            Me.lblInfInfo.Text = ""

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub mnuCopy_regno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuCopy_regno.Click
        Clipboard.Clear()
        Clipboard.SetText(Me.lblRegNo.Text)
    End Sub
End Class
