Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("AxAckPatientInfo")> 
<Assembly: AssemblyDescription("MEDI@CK.NET LIS 프로그램")> 
<Assembly: AssemblyCompany("ACK Co.,Ltd.")> 
<Assembly: AssemblyProduct("MEDI@CK .Net v3")> 
<Assembly: AssemblyCopyright("Copyrightⓒ 2010 ACK Co.,Ltd. All rights reserved")> 
<Assembly: AssemblyTrademark("MEDI@CK")> 
<Assembly: ComVisible(False)> 

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("1ef03adc-0d4c-4522-9b93-a51a8857b4d1")> 
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
'Version 3.1.10.233 --> 2012/08/29 : 외래채혈에서 예약일 추가, 미생물 양성자조회 쿼리튜닝, 최종보고 조회 쿼리 수정 내역 있는 것만 표시(AxAckCelloctor, LISAPP, AxAckResultView, AxAckPatinfo) 아직 배포 안함.
'Version 3.1.10.313 --> 2013/06/20 : DB 접속 변경
'Version 3.1.10.330 --> 2017/01/12 : 결과 조회 출력지 내용 수정
'Version 3.1.10.331 --> 2018/01/10 : 항응고제 내역 채혈 상단정보 특이사항 추가
'Version 3.1.10.332 --> 2018/01/10 : 항응고제 내역 표시변경
'Version 3.1.10.333 --> 2018/02/27 : vs2010 upgrade
'Version 3.1.10.334 --> 2018/03/02 : 항응고제 성분 추가 표시 
'Version 3.1.10.335 --> 2018/05/16 : 항응고제 투여일수 수정 
'Version 3.1.10.336 --> 2018/05/17 : dur오류 수정 
'Version 3.1.10.337 --> 2019/11/20 : 배포
'Version 3.1.10.338 --> 2020/05/25 : 혈액종양 진단명일때 색표시되도록
'Version 3.1.10.339 --> 2020/11/24 : 등록번호 프로퍼티 추가
'Version 3.1.10.340 --> 2021/03/22 : 오류로 인한 재배포
'Version 3.1.10.341 --> 2021/06/22 : 혈액은행 최근검사 결과 최종보고일시 -> 접수 일시로 변경
'Version 3.1.10.342 --> 2021/06/25 : 등록번호 조회 시 조회 되지 않는 부분 수정 
'Version 3.1.10.343 --> 2021/07/26 : 혈액은행 최근 검사 항목 LG126 추가 

<Assembly: AssemblyVersion("3.1.10.343")> 

