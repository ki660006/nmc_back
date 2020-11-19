Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("LISV")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("ACK")> 
<Assembly: AssemblyProduct("LISV")> 
<Assembly: AssemblyCopyright("Copyright © ACK 2011")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)>

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("52ef4f63-24e1-4b8a-b9c9-8513bb967c7c")> 

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
'Version 3.1.10.228 --> 2012/07/10 : 미생물 누적결과 오류
'Version 3.1.10.233 --> 2012/08/29 : 미생물 누적결과 오류
'Version 3.1.10.313 --> 2013/06/20 : DB 접속 변경
'Version 3.1.10.318 --> 2013/09/10 : 누적결과 조회 결과간격 (재배포), 항생제 위치 고정
'Version 3.1.10.319 --> 2013/12/05 : 환자이름조회 오류 수정 
'Version 3.1.10.320 --> 2014/02/09 : 누적결과 조회안되서 과거결과 테이블임시로 막음
'Version 3.1.10.321 --> 2014/03/10 : 누적결과 재검표시 수정
'Version 3.1.10.322 --> 2014/07/23 : 누적결과 신생아 나이표시 변경 
'Version 3.1.10.324 --> 2017/09/22 : 누적결과조회 화면 결핵균검사이고 Positive일때 색깔표시.
'Version 3.1.10.325 --> 2017/09/22 : 누적결과조회 화면 결핵균검사이고 Positive일때 색깔표시.(red-> ornage
'Version 3.1.10.326 --> 2018/02/27 : vs2010
'Version 3.1.10.327 --> 2018/08/21 : 누적결과 조회에서 리팜핀내성 결과 DETected 결과 색깔
'Version 3.1.10.328 --> 2019/10/10 : 누적결과 조회에서 결핵균은 3년치 조회되게 수정
'Version 3.1.10.329 --> 2019/10/10 : 누적결과 조회에서 결핵균은 3년치 조회되게 수정
<Assembly: AssemblyVersion("3.1.10.329")> 
