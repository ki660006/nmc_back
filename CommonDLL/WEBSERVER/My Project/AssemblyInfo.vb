Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("WEBSERVER")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("")> 
<Assembly: AssemblyProduct("WEBSERVER")> 
<Assembly: AssemblyCopyright("Copyright ©  2013")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)>

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("8a5956d4-dfc5-49cf-99d7-af253a28efc1")> 

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
'<Assembly: AssemblyFileVersion("1.0.0.0")> 
'Version 3.1.10.313 --> 2013/06/20 : DB 접속 변경
'Version 3.1.10.336 --> 2013/07/16 : AxRichTextBox(글씨 버그 수정), LISAPP(혈액은행 접수 취소 후 접수시 버그 수정), 미생물 추가처방(LISM, LISR)
'Version 3.1.10.342 --> 2013/07/29 : lism, WEbserver, common
'Version 3.1.10.344 --> 2015/05/14 : 추가처방 TEST완료후 배포
'Version 3.1.10.345 --> 2017/08/17 : 병원체검사 등록 web연동 추가 
'Version 3.1.10.346 --> 2017/09/04 : 병원체검사 오픈 배포
'Version 3.1.10.347 --> 2018/02/27 : vs2010
'Version 3.1.10.348 --> 2019/08/20 : 병동조회 속도 개선
'Version 3.1.10.349 --> 2019/08/26 : 병원체신고 테스트->실사용 수정
'Version 3.1.10.350 --> 2019/10/16 : 채혈화면 조회 수정

<Assembly: AssemblyVersion("3.1.10.350")> 
