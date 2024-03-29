﻿Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("LISF")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("ACK")> 
<Assembly: AssemblyProduct("LISF")> 
<Assembly: AssemblyCopyright("Copyright © ACK 2011")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)>

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("98c46088-38b9-47db-ad90-4c384d3c8f00")>

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
'Version 3.1.10.232 --> 2012/08/08 : 작업그룹에서 검사표시 오류
'Version 3.1.10.234 --> 2012/09/12 : 기초자료버그 발견(F01, LISAPP)
'Version 3.1.10.251 --> 2012/12/26 : 자동소견버그 수정
'Version 3.1.10.252 --> 2013/01/15 : 계산식 지수연산기능추가
'Version 3.1.10.313 --> 2013/06/20 : DB 접속 변경
'Version 3.1.10.317 --> 2013/08/07 : 작업그룹 코드 선택 시 베터리 코드 선택 가능하도록 수정
'Version 3.1.10.319 --> 2013/09/09 : Alert Rule 버그 수정(LISF)
'Version 3.1.10.320 --> 2014/02/13 : 세부검사 등록안되던것(검사법,주의내용,임상적의의 ) 수정(LISF)
'Version 3.1.10.321 --> 2014/02/14 : 세부검사 등록안되던것(검사법,주의내용,임상적의의 ) 수정(LISF)
'Version 3.1.10.322 --> 2016/08/03 : 검체(처방명) 길이 수정(LISF)
'Version 3.1.10.323 --> 2017/06/21 : 참고치 excel 추가.
'Version 3.1.10.324 --> 2017/06/23 : 참고치 excel 추가.버그 수정 
'Version 3.1.10.325 --> 2017/07/17 : 검사코드등록 - > 식사관련 채혈 주의사항 추가
'Version 3.1.10.326 --> 2017/08/17 : 병원체등록 테스트 추가 
'Version 3.1.10.327 --> 2017/09/04 : 병원체등록 추가수정
'Version 3.1.10.328 --> 2017/09/12 : 검사항목별 검사코드 추가
'Version 3.1.10.329 --> 2017/09/22 : 검사코드 등록 화면에서 채혈주의사항 팝업 체크박스 컬럼 추가.
'Version 3.1.10.330 --> 2018/02/13 : 병원체코드 중복로우조회 수정 , 개별등록부분 , 마스터 기타 오류 수정 
'Version 3.1.10.331 --> 2018/02/27 : vs2010
'Version 3.1.10.333 --> 2018/08/21 : 결과코드에 특수문자 추가 
'Version 3.1.10.334 --> 2018/09/20 : FDF34에 사유코드 텍스트박스 입력제한 (4자리 > 6자리) 수정
'Version 3.1.10.335 --> 2019/05/17 : 검사코드관리 세부정보에 검사의뢰정보 디자인 추가
'Version 3.1.10.336 --> 2019/08/20 : 용기뚜껑색 마스터 추가
'Version 3.1.10.337 --> 2019/12/30 : 배양균명 길이 수정 60->90
'Version 3.1.10.338 --> 2020/03/06 : 특수보고서 소견 기초마스터 화면 추가
'Version 3.1.10.339 --> 2020/03/17 : 특수보고서 소견 기초마스터 화면 수정
'Version 3.1.10.340 --> 2020/04/25 : 특수보고서 소견 수정 오류 수정
'Version 3.1.10.341 --> 2020/08/04 : 용기이미지 사이즈모드 변경
'Version 3.1.10.342 --> 2020/08/06 : 검사 체취및 의뢰시 주의사항 텍스트박스 maxlength 증설
'Version 3.1.10.343 --> 2020/09/03 : 검사마스터에 검체단위 추가
'Version 3.1.10.344 --> 2020/10/12 : 검사코드관리 검사의뢰서/동의서, 시행처 다중선택 추가
'Version 3.1.10.345 --> 2020/11/30 : 검사의뢰지침 세부검사 항목 설정 화면 추가개발
'Version 3.1.10.346 --> 2021/07/19 : 결과코드, 혈액은행 오류수정
'Version 3.1.10.347 --> 2021/08/18 : 결과코드 alter추가 , 배양균 색변경 항목 추가
'Version 3.1.10.348 --> 2021/09/01 : 결과코드 c안나오는 현상 수정
'Version 3.1.10.349 --> 2021/09/08 : 결과코드 Alter 삭제
'Version 3.1.10.350 --> 2021/11/29 : 수혈제제 관리 코드화, 기초마스터 동의서 의뢰서 종류 텍스트 박스 추가 
'Version 3.1.10.351 --> 2021/12/20 : 혈액 성분제제 TAT 관련 내용 추가(베타)
'Version 3.1.10.352 --> 2022/02/08 : 혈액 성분제제 TAT 관련 내용 추가(베타)(롤백)
'Version 3.1.10.353 --> 2022/04/26 : 결과코드 기초마스터 수정
'Version 3.1.10.354 --> 2022/06/28 : 검사코드관리 시행처 문구 수정
'Version 3.1.10.355 --> 2022/07/04 : 소견 허용길이 증설 및 2000byte 체크 추가

<Assembly: AssemblyVersion("3.1.10.355")>