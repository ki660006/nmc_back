Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("POPUPWIN")> 
<Assembly: AssemblyDescription("MEDI@CK.NET LIS 프로그램")> 
<Assembly: AssemblyCompany("ACK Co.,Ltd.")> 
<Assembly: AssemblyProduct("MEDI@CK .Net v3")> 
<Assembly: AssemblyCopyright("Copyrightⓒ 2010 ACK Co.,Ltd. All rights reserved")> 
<Assembly: AssemblyTrademark("MEDI@CK")> 
<Assembly: ComVisible(False)> 

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("ec684db8-59ca-47ad-bba5-5f80c1238665")> 
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
'Version 3.1.10.228 --> 2012/07/10 : 종합검증 소견선택 추가
'Version 3.1.10.232 --> 2012/08/02 : 종합검증버그(POPUPWIN, SYSIF01)
'Version 3.1.10.233 --> 2012/12/26 : 특수검사모듈 추가 FGPOPUPST_CYTOSPIN.vb(POPUPWIN)
'Version 3.1.10.313 --> 2013/06/20 : DB 접속 변경
'Version 3.1.10.319 --> 2013/08/02 : comment 스펠링 틀린 것 수정(FGPOPUPST_CYTOSPIN.vb)
'Version 3.1.10.321 --> 2014/05/26 : 종합검증 연동프로그램 쿼리조회 오류수정(FGPOPUPST_VRST2.vb)
'Version 3.1.10.325 --> 2016/08/31 : 메르스, 지카 바이러스 모듈 추가(FGPOPUPST_MERS.vb,FGPOPUPST_ZIKA.vb)
'Version 3.1.10.326 --> 2016/08/31 : 메르스, 지카 바이러스 모듈 추가(FGPOPUPST_MERS.vb,FGPOPUPST_ZIKA.vb)
'Version 3.1.10.327 --> 2016/09/05 : 메르스, 지카 바이러스 모듈 추가(FGPOPUPST_MERS.vb,FGPOPUPST_ZIKA.vb)(재배포)
'Version 3.1.10.328 --> 2017/02/23 : Cytospin 모듈 수정.
'Version 3.1.10.329 --> 2017/03/14 : Cytospin 모듈 수정.
'Version 3.1.10.330 --> 2017/09/05 : pbs 특수폼 명칭수정
'Version 3.1.10.331 --> 2018/02/21 : 특수검사 모듈 검체코드 수정
'Version 3.1.10.332 --> 2018/02/27 : vs2010
'Version 3.1.10.333 --> 2018/07/05 : 지카 바이러스 검사명 수정
'Version 3.1.10.334 --> 2018/08/07 : Cytospin 특수문자 추가 수정
'Version 3.1.10.335 --> 2019/02/27 : 특수보고서 양식 추가
'Version 3.1.10.336 --> 2019/05/29 : 특수검사 모듈 aptt, pt 양식 수정6
'Version 3.1.10.337 --> 2020/02/27 : 코로나 특수보고서 모듈 추가
'Version 3.1.10.338 --> 2020/03/06 : 코로나 특수보고서 모듈 요구사항 반영
'Version 3.1.10.339 --> 2020/03/17 : 코로나 특수보고서 모듈 수정
'Version 3.1.10.340 --> 2020/05/18 : 코로나 특수보고서 모듈 수정
'Version 3.1.10.341 --> 2020/05/27 : 코로나 특수보고서 모듈 수정
'Version 3.1.10.342 --> 2020/05/28 : 코로나 특수보고서 모듈 수정
'Version 3.1.10.343 --> 2020/08/04 : 코로나 특수보고서 모듈 수정
'Version 3.1.10.344 --> 2020/08/05 : 코로나 특수보고서 모듈 수정
'Version 3.1.10.345 --> 2020/08/24 : 코로나 특수보고서 모듈 수정
'Version 3.1.10.346 --> 2020/10/08 : 코로나 특수보고서 모듈 수정
'Version 3.1.10.347 --> 2020/10/08 : 코로나 특수보고서 모듈 수정
'Version 3.1.10.348 --> 2020/10/12 : 코로나 특수보고서 모듈 수정
'Version 3.1.10.349 --> 2020/10/13 : 코로나 특수보고서 모듈 수정
'Version 3.1.10.350 --> 2020/10/22 : 코로나 특수보고서 모듈 수정

<Assembly: AssemblyVersion("3.1.10.350")> 
