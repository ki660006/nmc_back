Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("LIST")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("ACK")> 
<Assembly: AssemblyProduct("LIST")> 
<Assembly: AssemblyCopyright("Copyright © ACK 2011")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)>

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("e7604d1f-f9d8-4d90-aaff-ff51733b46e3")>

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
'Version 3.1.10.214 --> 2012/04/12 : YEJ 검사통계에서 외래,입원 구분 통계에서 전체 건수가 틀린 문제 수정(LISAPP, LIST)
'Version 3.1.10.229 --> 2012/07/10 : 목표TAT 포함
'Version 3.1.10.313 --> 2013/06/20 : DB 접속 변경
'Version 3.1.10.337 --> 2013/12/10 : 미생물 통계조회 접수일자 통계수정
'Version 3.1.10.338 --> 2014/02/13 : 통계 rst_tm 받아올때 소수점 버림 
'Version 3.1.10.339 --> 2014/07/04 : TAT관리 평균시간계산 오류수정
'Version 3.1.10.342 --> 2016/02/25 : TAT관리 여러Item 조회 가능 하게 수정.
'Version 3.1.10.343 --> 2016/02/25 : 재배포
'Version 3.1.10.344 --> 2016/02/25 : 재배포
'Version 3.1.10.345 --> 2016/05/19 : TAT통계 구간별 카운트 조건 수정
'Version 3.1.10.346 --> 2016/05/23 : 검체통계 조회 추가
'Version 3.1.10.347 --> 2016/07/06 : TAT관리 출력물 검사항목명 추가 
'Version 3.1.10.348 --> 2017/01/18 : TAT통계 유효건수, 전체건수 안 맞는 부분 수정.
'Version 3.1.10.351 --> 2017/02/23 : TAT관리 TAT Overtime 이상이 아닌 초과로 수정.
'Version 3.1.10.352 --> 2017/04/18 : TAT통계 기준 TAT 시간추가 
'Version 3.1.10.354 --> 2017/05/30 : TAT통계 유효건수와 전체건수 계산 오류 수정 
'Version 3.1.10.355 --> 2017/06/05 : TAT통계 초과건수 갯수 오류 
'Version 3.1.10.356 --> 2017/07/13 : TAT통계 검사항목 조회오류 , TAT 관리 목표시간 오류 수정 
'Version 3.1.10.357 --> 2018/02/27 : vs2010
'Version 3.1.10.358 --> 2018/11/22 : 미생물 검사통계 검체 여러개 선택 가능하도록 수정.
'Version 3.1.10.359 --> 2019/01/29 : 미생물 통계 화면 mODIFY
'Version 3.1.10.360 --> 2019/05/09 : TAT통계 기준 버그 수정
'Version 3.1.10.361 --> 2019/07/04 : TAT관리 화면정리 검사항목 TAG 비워주도록
'Version 3.1.10.362 --> 2019/11/20 : 배포
'Version 3.1.10.363 --> 2020/06/23 : TAT관리 중간보고/최종보고 구분 추가
'Version 3.1.10.364 --> 2020/08/04 : 미생물 결핵통계 수정
'Version 3.1.10.365 --> 2020/08/06 : 미생물 결핵통계 검체 전체->선택시 저장된 설정 불러와서 체크
'Version 3.1.10.366 --> 2022/01/20 : 검체통계 조회에서 toexcel 안되는 문제 수정

<Assembly: AssemblyVersion("3.1.10.366")>
