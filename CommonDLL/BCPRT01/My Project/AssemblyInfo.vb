Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("BCPRT01")> 
<Assembly: AssemblyDescription("MEDI@CK.NET LIS 프로그램")> 
<Assembly: AssemblyCompany("ACK Co.,Ltd.")> 
<Assembly: AssemblyProduct("MEDI@CK .Net v3")> 
<Assembly: AssemblyCopyright("Copyrightⓒ 2010 ACK Co.,Ltd. All rights reserved")> 
<Assembly: AssemblyTrademark("MEDI@CK")> 
<Assembly: ComVisible(False)> 

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("7148cab7-c374-4409-83b9-991494e43100")> 
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
'Version 3.1.10.100 --> 2011/01/01 : 3.1.10.100으로 초기 셋팅
'Version 3.1.10.187 --> 2012/12/17 : 혈액바코드 3차부적합판정 수정
'Version 3.1.10.187 --> 2014/07/31 : SATO400 바코드 감염정보 표시 변경
'Version 3.1.10.329 --> 2014/08/28 : 루칸/세우 바코드 감염정보 표시 변경
'Version 3.1.10.331 --> 2014/10/10 : SATO400/루칸/세우 바코드 감염정보 표시 변경(폰트변경)
'Version 3.1.10.336 --> 2015/06/04 : SATO400/루칸바코드 감염정보 표시 재변경(폰트변경) 및  바코드 출력일시 표시 위치 변경(우측 상단->우측 하단), 상호 수정
'Version 3.1.10.337 --> 2016/09/22 : SATO400 미출력바코드에 환자번호 바코드 추가
'Version 3.1.10.338 --> 2016/10/20 : SATO400 미출력바코드 글자 크기 변경
'Version 3.1.10.339 --> 2017/10/30 : 혈액은행 혈액바코드 수정.
'Version 3.1.10.340 --> 2017/10/30 : 혈액은행 혈액바코드 수정.
'Version 3.1.10.341 --> 2017/10/30 : 혈액은행 혈액바코드 수정.
'Version 3.1.10.342 --> 2017/10/30 : 혈액은행 혈액바코드 수정.
'Version 3.1.10.342 --> 2017/10/30 : 혈액은행 혈액바코드 수정.
'Version 3.1.10.344 --> 2018/02/06 : 혈액은행 sewoo 바코드 수정 
'Version 3.1.10.345 --> 2018/02/27 : vs2010 upgarde
'Version 3.1.10.346 --> 2018/08/21 : 응급바코드 임시배포
'Version 3.1.10.347 --> 2019/04/25 : 바코드 환자 혈액형 여부 표시 (SATO, LUKHAN)
'Version 3.1.10.348 --> 2019/07/30 : 바코드 혈액은행 채혈자 박스추가(sato, lukhan은 해야함)
'Version 3.1.10.349 --> 2019/08/20 : 바코드 혈액은행 채혈자 박스추가(sato, lukhan은 해야함)
'Version 3.1.10.350 --> 2019/11/20 : 배포
'Version 3.1.10.351 --> 2020/08/05 : LUKHAN >> 미채혈바코드, 검사분야 표시 수정
'Version 3.1.10.352 --> 2020/11/23 : LUKHAN, SATO 자체응급 바코드 R표시, 크기조정2
'Version 3.1.10.353 --> 2020/12/10 : LUKHAN 바코드 특정장비 리딩문제로 원복
'Version 3.1.10.354 --> 2020/12/22 : LUKHAN 바코드 특정장비 리딩문제로 원복
'Version 3.1.10.355 --> 2021/01/27 : 접수시 바코드 출력되도록 수정 

<Assembly: AssemblyVersion("3.1.10.355")> 
