Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("BCPAPP")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("ACK")> 
<Assembly: AssemblyProduct("BCPAPP")> 
<Assembly: AssemblyCopyright("Copyright © ACK 2011")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)>

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("9bf152cf-ba3b-4f82-844e-a55760c108fc")> 

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
'Version 3.1.10.189 --> 2013/01/01 : 배지바코드 날짜 쿼리 오류 수정  
'Version 3.1.10.313 --> 2013/06/20 : DB 접속 변경
'Version 3.1.10.332 --> 2013/07/10 : w/k 바코드 재출력 수정
'Version 3.1.10.333 --> 2013/11/27 : 바코드 출력시 & 표시 수정 
'Version 3.1.10.334 --> 2014/08/28 : 병동채혈바코드 재출력시 감염정보 오류 수정
'Version 3.1.10.335 --> 2018/02/27 : vs2010
'Version 3.1.10.336 --> 2018/08/21 : 응급바코드 임시배포
'Version 3.1.10.337 --> 2019/04/25 : 환자 혈액형 여부 바코드 표시
'Version 3.1.10.338 --> 2020/08/04 : 세포면역 배지바코드 출력 추가
'Version 3.1.10.339 --> 2020/11/23 : 바코드 재출력시 자체응급 R표시
'Version 3.1.10.340 --> 2020/12/01 : 핵의학 자체응급 컬럼 공백으로 추가
'Version 3.1.10.341 --> 2021/02/22 : 배지바코드 5장 출력 될 수 있도록 구현

<Assembly: AssemblyVersion("3.1.10.341")> 
