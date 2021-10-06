Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("RISAPP")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("ACK")> 
<Assembly: AssemblyProduct("RISAPP")> 
<Assembly: AssemblyCopyright("Copyright © ACK 2011")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)>

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("897f0093-a1f4-4d53-bba8-cfabcbb257ea")> 

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
'Version 3.1.10.313 --> 2013/06/20 : DB 접속 변경
'Version 3.1.10.335 --> 2014/05/29 : TAT 조회 , TAT 통계 , TAT 관리 오류 수정
'Version 3.1.10.336 --> 2014/09/22 : TAT 조회 소견등록오류수정
'Version 3.1.10.337 --> 2014/10/02 : 검사통계 검진, 응급 안나오는것 수정
'Version 3.1.10.338 --> 2015/08/27 : 검사항목별결과조회 이상치결과 붉은색으로 수정 
'Version 3.1.10.339 --> 2015/08/27 : WL조회화면 WL순서로 조회 쿼리수정  
'Version 3.1.10.340 --> 2015/11/19 : lhj 접수 / WL 생성 및 조회 화면에서 검사항목을 제외한 컬럼 클릭 시, 이전 결과, 이전 결과일, 처방 일시 초기화. 
'Version 3.1.10.341 --> 2018/02/27 : vs2010 
'Version 3.1.10.342 --> 2021/06/17 : 특정검사 바코드 음영처리

<Assembly: AssemblyVersion("3.1.10.342")> 
