Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("AxAckCollector")> 
<Assembly: AssemblyDescription("MEDI@CK.NET LIS 프로그램")> 
<Assembly: AssemblyCompany("ACK Co.,Ltd.")> 
<Assembly: AssemblyProduct("MEDI@CK .Net v3")> 
<Assembly: AssemblyCopyright("Copyrightⓒ 2010 ACK Co.,Ltd. All rights reserved")> 
<Assembly: AssemblyTrademark("MEDI@CK")> 
<Assembly: ComVisible(False)> 

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("4fbba767-1d1f-46bf-8b33-50b0c3a0a3f4")> 
'<Assembly: AssemblyFileVersion("1.0.0.0")> 

' 어셈블리의 버전 정보는 다음 네 가지 값으로 구성됩니다.
'
'      주 버전
'      부 버전
'      빌드 번호
'      수정 버전
'
' 모든 값을 지정하거나 아래와 같이 '*'를 사용하여 빌드 번호 및 수정 버전이 자동으로
' 지정되도록 할 수 있습니다.
' <Assembly: AssemblyVersion("1.0.*")> 
'Version 3.1.10.100 --> 2011/01/01 : 3.1.10.100으로 초기 셋팅
'Version 3.1.10.222 --> 2012/06/04 : 중복채혈오류 수정
'Version 3.1.10.233 --> 2012/08/29 : 외래채혈에서 예약일 추가, 미생물 양성자조회 쿼리튜닝(AxAckCelloctor, LISAPP) 아직 배포 안함.
'Version 3.1.10.313 --> 2013/06/20 : DB 접속 변경
'Version 3.1.10.330 --> 2015/10/16 : 오픈카드수정
'Version 3.1.10.331 --> 2017/07/17 : 식사관련 채혈주의
'Version 3.1.10.333 --> 2017/07/18 : 식사관련 채혈주의 오류로 체크로직 추가
'Version 3.1.10.334 --> 2017/09/22 : 채혈주의사항 msg박스로 채혈화면에 나오게 수정.
'Version 3.1.10.335 --> 2018/02/27 : vs2010 upgrade
'Version 3.1.10.336 --> 2018/08/21 : 응급바코드 임시배포
'Version 3.1.10.337 --> 2019/04/25 : 바코드 혈액형 여부 표시
'Version 3.1.10.338 --> 2019/08/20 : 용기뚜껑색표시
'Version 3.1.10.339 --> 2019/11/20 : 2019/11/20
'Version 3.1.10.340 --> 2020/05/25 : 혈액종양 진단명일때 색깔 표시
'Version 3.1.10.341 --> 2020/06/23 : 자체응급 표시
'Version 3.1.10.342 --> 2020/08/06 : 오른쪽클릭->검사정보 클릭시 검사의뢰지침으로 열리도록 처리
'Version 3.1.10.343 --> 2021/06/15 : 특정검사 바코드 검사명 음영처리

<Assembly: AssemblyVersion("3.1.10.343")> 
