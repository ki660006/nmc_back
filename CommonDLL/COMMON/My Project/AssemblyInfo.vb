Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("COMMON")> 
<Assembly: AssemblyDescription("MEDI@CK.NET LIS 프로그램")> 
<Assembly: AssemblyCompany("ACK Co.,Ltd.")> 
<Assembly: AssemblyProduct("MEDI@CK .Net v3")> 
<Assembly: AssemblyCopyright("Copyrightⓒ 2010 ACK Co.,Ltd. All rights reserved")> 
<Assembly: AssemblyTrademark("MEDI@CK")> 
<Assembly: ComVisible(False)> 

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("8b719866-0cc8-4341-8883-44b476ce7361")> 
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
'Version 3.1.10.221 --> 2012/12/17 : 혈액바코드 3차 부적합 판정 수정
'Version 3.1.10.222 --> 2013/01/15 : 계산식 지수연산기능추가
'Version 3.1.10.313 --> 2013/06/20 : DB 접속 변경
'Version 3.1.10.342 --> 2013/07/29 : lism, WEbserver, common
'Version 3.1.10.359 --> 2013/09/09 : Alert Rule 버그 수정(LISF), COMMON
'Version 3.1.10.360 --> 2014/03/10 : 미생물 IF 쪽에서 STU_rstdt 추가 COMMON
'Version 3.1.10.361 --> 2014/10/08 : 결과조회 출력물 핵의학과 표시
'Version 3.1.10.362 --> 2015/05/14 : 추가처방 TEST완료후 배포
'Version 3.1.10.363 --> 2015/05/14 : 추가처방 TEST완료후 배포가 잘이루어지지 않아 재배포(COMMON)
'Version 3.1.10.364 --> 2016/02/25 : 질병관리 본부 조회 오류 배포
'Version 3.1.10.365 --> 2016/02/25 : 재배포
'Version 3.1.10.367 --> 2016/04/10 : 양성자조회 접수일시 추가 
'Version 3.1.10.368 --> 2017/06/21 : 혈액은행 폐기시 15분 -> 30분으로 수정
'Version 3.1.10.369 --> 2017/08/17 : 질병관리본부 병원체 구조체 추가
'Version 3.1.10.370 --> 2017/09/04 : 질병관리본부 병원체 추가수정 
'Version 3.1.10.371 --> 2017/10/25 : 질병관리본부 병원체 추가수정 (전역변수 추가)
'Version 3.1.10.372 --> 2018/02/27 : vs2010 upgrade
'Version 3.1.10.373 --> 2018/03/30 : 위탁검사
'Version 3.1.10.374 --> 2018/08/21 : 응급바코드 임시배포
'Version 3.1.10.375 --> 2019/04/25 : 환자 혈액형 여부 바코드 표시
'Version 3.1.10.376 --> 2019/11/20 : 배포
'Version 3.1.10.377 --> 2020/02/27 : 코로나 특수보고서 모듈 추가
'Version 3.1.10.378 --> 2020/05/25 : 혈액종양 진단명 색깔 표시
'Version 3.1.10.379 --> 2020/06/02 : CVR 등록 CLASS 추가
'Version 3.1.10.380 --> 2020/06/23 : CVR 등록 class 추가
'Version 3.1.10.382 --> 2020/06/30 : CVR 등록 CLASS 단위추가, 녹십자 자동연동 CLASS 추가
'Version 3.1.10.382 --> 2020/06/30 : 결과이력 삭제 CLASS 추가
'Version 3.1.10.383 --> 2020/07/06 : 바이트수 자르는 function 추가
'Version 3.1.10.384 --> 2020/07/09 : 바이트수 자르는 function 추가
'Version 3.1.10.385 --> 2020/11/30 : 검사의뢰지침 세부검사 항목 저장에 따른 class 추가
'Version 3.1.10.386 --> 2021/02/22 : 배지바코드 다중출력되도록 수정
'Version 3.1.10.387 --> 2021/03/16 : 검사자간 공유사항 추가, wbc diff log 생성 로직 추가
'Version 3.1.10.388 --> 2021/04/19 : QC데이터 연동
'Version 3.1.10.389 --> 2021/06/15 : 특정검사 바코드 음영처리
'Version 3.1.10.390 --> 2021/06/22 : TAT차일드 코드 포함하여 조회 될수 있도록 수정

<Assembly: AssemblyVersion("3.1.10.390")> 
