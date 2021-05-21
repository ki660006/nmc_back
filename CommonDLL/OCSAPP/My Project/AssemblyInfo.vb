Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("OCSAPP")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("ACK")> 
<Assembly: AssemblyProduct("OCSAPP")> 
<Assembly: AssemblyCopyright("Copyright © ACK 2011")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)>

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("638bd618-bd0d-4eaf-8057-cc4dbff41b16")> 

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
'Version 3.1.10.214 --> 2012/04/12 : YEJ 건강검진실 채혈 수정(LISC, OCSAPP)
'Version 3.1.10.226 --> 2012/06/26 : 검체 history에서 채혈전 취소된 것은 표시안되게 수정
'Version 3.1.10.329 --> 2013/10/28 : 주진단 조회조건 수정 (ocsapp)
'Version 3.1.10.330 --> 2016/06/21 : 종합검증 상단정보 주치의 조회 관련 수정  (ocsapp)
'Version 3.1.10.331 --> 2018/01/10 : 항응고제 처방이력 내용 상단정보에 추가
'Version 3.1.10.332 --> 2018/01/10 : 항응고제 처방이력 표시수정
'Version 3.1.10.333 --> 2018/02/27 : vs2010
'Version 3.1.10.334 --> 2018/05/16 : 항응고제 표시 투여날짜변경 
'Version 3.1.10.335 --> 2018/05/31 : 수혈처방 선조회 화면 추가 
'Version 3.1.10.336 --> 2018/06/01 : 수혈처방 선조회 쿼리수정 (prep 보이게)
'Version 3.1.10.337 --> 2018/08/31 : 수혈처방 선조회 출고갯수 혈액형 수정 
'Version 3.1.10.338 --> 2018/08/31 : 수혈처방 선조회 출고갯수 혈액형 수정 
'Version 3.1.10.339 --> 2018/10/24 : 수혈처방 조회 화면에서 의뢰수랑이 일치하지 않는 문제 수정 요청
'Version 3.1.10.340 --> 2018/10/25 : 수혈처방 조회 화면에서 의뢰수랑이 일치하지 않는 문제 수정 요청 재배포
'Version 3.1.10.341 --> 2019/04/25 : 환자 혈액형 여부 조회 추가
'Version 3.1.10.342 --> 2020/05/25 : 혈액종양 진단명일때 색표시
'Version 3.1.10.343 --> 2020/06/02 : 혈액종양 진단명추가
'Version 3.1.10.344 --> 2020/08/04 : 수혈접수처방조회 쿼리 수정, 혈액종양 진단 판정받은 환자일때 진단명 색표시
'Version 3.1.10.345 --> 2020/11/23 : 수혈접수처방조회 혈액불출요청시간 수정

<Assembly: AssemblyVersion("3.1.10.345")> 
