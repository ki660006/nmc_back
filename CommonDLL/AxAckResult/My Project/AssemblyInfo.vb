﻿Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("AxAckResult")> 
<Assembly: AssemblyDescription("MEDI@CK.NET LIS 프로그램")> 
<Assembly: AssemblyCompany("ACK Co.,Ltd.")> 
<Assembly: AssemblyProduct("MEDI@CK .Net v3")> 
<Assembly: AssemblyCopyright("Copyrightⓒ 2010 ACK Co.,Ltd. All rights reserved")> 
<Assembly: AssemblyTrademark("MEDI@CK")> 
<Assembly: ComVisible(False)> 

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("7a754dad-549a-4992-98f4-daff039710fe")> 
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
'Version 3.1.10.211 --> 2012/04/02 : 
'Version 3.1.10.218 --> 2012/05/17 : 결과입력화면에서 엔터 시 다음줄로 안 넘어가는 현상 수정)
'Version 3.1.10.228 --> 2012/07/10 : SLIP 표시
'Version 3.1.10.229 --> 2012/07/24 : 결과등록에서 작업번호표시 오류(AxResult, LISM)
'Version 3.1.10.234 --> 2012/09/25 : 미생물결과등록에서 진단내역 추가에 따른 수정
'Version 3.1.10.236 --> 2012/10/09 : 미생물 SMS전송 메시지 변경,멀티라인입력시 ADD기능 추가
'Version 3.1.10.237 --> 2012/12/06 : 멀티라인입력시 ADD기능 버그 수정
'Version 3.1.10.239 --> 2013/01/16 : 항생제 삭제기능 추가,계산식 지수연산 기능
'Version 3.1.10.313 --> 2013/06/20 : DB 접속 변경
'Version 3.1.10.342 --> 2013/07/29 : SMS 전송 수정
'Version 3.1.10.344 --> 2013/08/26 : SMS 전송 의사명 입력창 한글키 설정, 미생물 결과입력 검사그룹
'Version 3.1.10.345 --> 2013/11/07 : 결과입력시 회색칸은 입력이나 lstcode 불러오기 불가 처리
'Version 3.1.10.346 --> 2013/11/18 : 엔터입력시 다음줄로 오류수정
'Version 3.1.10.347 --> 2014/07/04 : 검사결과화면에서 감염정보 색깔표시
'Version 3.1.10.347 --> 2014/07/04 : 검사결과화면에서 감염정보 색깔표시
'Version 3.1.10.348 --> 2014/07/10 : SMS 전송 발신자 번호 표시 수정. 
'Version 3.1.10.350 --> 2014/11/26 : 부적합등록 자동 체크 추가
'Version 3.1.10.355 --> 2016/11/22 : 결핵검사 진행시 CBC검사 항목 결과 소견으로 가져오도록 수정.
'Version 3.1.10.356 --> 2016/12/20 : Parent코드 단독 검사 시 최종보고 안 되는 버그 수정.
'Version 3.1.10.357 --> 2017/04/11 : 미생물 SMS 전송시 02 앞에 붙도록 수정 
'Version 3.1.10.358 --> 2017/04/30 : SMS 저장기능 추가 
'Version 3.1.10.359 --> 2017/07/06 : 결핵검사 진행 시 CBC검사 항목 결과 소견가져오기 추가(LI613)
'Version 3.1.10.360 --> 2018/02/27 : vs2010 upgrade
'Version 3.1.10.361 --> 2018/04/10 : 결과 소수점 변환 replace 추가
'Version 3.1.10.362 --> 2018/05/16 : 최종보고 수정시 소견 변환
'Version 3.1.10.364 --> 2018/06/14 : key입력시 return 소견 문구 변경 
'Version 3.1.10.365 --> 2018/06/28 : Diff count 문구 변경 , 세부검사에서 특수검사 보이기
'Version 3.1.10.366 --> 2018/06/29 : 관련검사 최근결과 lr010m, lm010m 결과 다 볼 수 있도록 수정
'Version 3.1.10.367 --> 2018/07/11 : 크리티컬 보고시 결과 팝업 체크
'Version 3.1.10.368 --> 2018/11/06 : 크리티컬 보고시 결과 팝업 체크
'Version 3.1.10.369 --> 2018/11/22 : 결과 화면 특이결과 조회 버튼 추가
'Version 3.1.10.370 --> 2019/01/29 : kEYPAD 입력 시 100 이상 입력 소리 변경
'Version 3.1.10.371 --> 2019/07/16 : LI611, LI612, LI613 검사일때 추가소견 내용 변경(하드코딩)
'Version 3.1.10.372 --> 2019/08/28 : 혈액은행 ABO초진환자 체크
'Version 3.1.10.373 --> 2019/11/20 : 2019/11/20
'Version 3.1.10.374 --> 2019/12/30 : AFB
'Version 3.1.10.375 --> 2019/12/31 : AFB
'Version 3.1.10.376 --> 2019/12/31 : AFB
'Version 3.1.10.377 --> 2020/01/09 : 특정검사 결과가 있을시 무조건 H표시되도록
'Version 3.1.10.378 --> 2020/05/25 : 혈액종양 진단명일때 색깔 표시
'Version 3.1.10.378 --> 2020/05/25 : 혈액종양 진단명일때 색깔 표시
'Version 3.1.10.379 --> 2020/06/02 : CVR 등록 추가
'Version 3.1.10.380 --> 2020/06/03 : CVR 목록 추가
'Version 3.1.10.381 --> 2020/06/03 : CVR 해상도 문제로 위치 조정
'Version 3.1.10.382 --> 2020/06/03 : CVR 해상도 문제로 위치 조정
'Version 3.1.10.383 --> 2020/06/23 : CVR 등록 결과값 없을때 안되도록 추가
'Version 3.1.10.384 --> 2020/06/30 : CVR 결과단위 추가, 결과코드에 대한 내용을 적용하는 부분
'Version 3.1.10.385 --> 2020/08/04 : 자체응급 화면표시, 혈액종양 진단 환자 진단명 색표시

<Assembly: AssemblyVersion("3.1.10.385")> 
