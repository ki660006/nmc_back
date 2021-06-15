Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("LISC")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("ACK")> 
<Assembly: AssemblyProduct("LISC")> 
<Assembly: AssemblyCopyright("Copyright © ACK 2011")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)>

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("23288e3e-543a-4ac9-8ba6-095737b56047")> 

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
'Version 3.1.10.233 --> 2012/08/29 : 외래채혈에서 예약일 추가, 미생물 양성자조회 쿼리튜닝, 최종보고 조회 쿼리 수정 내역 있는 것만 표시(AxAckCelloctor, LISAPP, AxAckResultView, AxAckPatinfo, LISB, LISV, LISC) 아직 배포 안함.
'Version 3.1.10.313 --> 2013/06/20 : DB 접속 변경
'Version 3.1.10.330 --> 2015/10/16 : 오픈카드수정 
'Version 3.1.10.331 --> 2017/07/17 : 식사관련채혈주의 추가
'Version 3.1.10.332 --> 2018/02/27 : vs2010
'Version 3.1.10.333 --> 2019/08/20 : 병동조회 속도 개선
'Version 3.1.10.334 --> 2019/11/29 : 11/29배포
'Version 3.1.10.335 --> 2020/04/13 : 외래채혈화면 tabindex 조정
'Version 3.1.10.336 --> 2021/03/20 : 외래채혈 주민등록번호 조회시 오류 수정
'Version 3.1.10.337 --> 2021/03/22 : 외래채혈 환자명으로 조회 시 오류 수정
'Version 3.1.10.338 --> 2021/06/15 : 특정검사 검사명 음영처리 

<Assembly: AssemblyVersion("3.1.10.338")> 

