Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("AxAckResultViewer")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("ACK")> 
<Assembly: AssemblyProduct("AxAckResultViewer")> 
<Assembly: AssemblyCopyright("Copyright © ACK 2010")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)>

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("0f16abdd-44d2-4c56-aef7-87b1e7495be8")>
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
'Version 3.1.10.233 --> 2012/08/29 : 
'Version 3.1.10.234 --> 2014/04/02 : ocs과거 결과 조회시 채혈일자누락으로 오류 수정 
'Version 3.1.10.235 --> 2014/05/08 : 일일보고서 출력시 진료과 표기 오류 수정 
'Version 3.1.10.236 --> 2014/10/08 : 결과조회 출력시 핵의학과 표시 
'Version 3.1.10.237 --> 2014/11/27 : 결과조회 출력시 핵의학과 표시 버그픽스
'Version 3.1.10.238 --> 2015/12/17 : 결과조회 미생물 항균제 표시 오류 수정
'Version 3.1.10.239 --> 2016/08/03 : 결과조회 출력시 검사명 조정 
'Version 3.1.10.242 --> 2016/10/07 : 결과조회 출력시 멀티라인 결과 볼 수 있도록 수정
'Version 3.1.10.243 --> 2016/01/12 : 결과조회 출력 내용 변경
'Version 3.1.10.244 --> 2018/02/20 : 결과조회 출력 시 위치 조정
'Version 3.1.10.245 --> 2018/02/27 : vs2010 upgrade
'Version 3.1.10.246 --> 2018/07/25 : 출력지 위치 조정 
'Version 3.1.10.247 --> 2019/11/29 : 11/29배포
'Version 3.1.10.248 --> 2021/03/11 : 일일보고서 출력시 바닥글과 본문이 겹쳐 써지는 현상 해결
'Version 3.1.10.249 --> 2021/03/15 : 일일보고서 출력시 바닥글과 본문이 겹쳐 써지는 현상 해결(재배포)
'Version 3.1.10.250 --> 2021/03/15 : 일일보고서 출력시 바닥글과 본문이 겹쳐 써지는 현상 해결(재배포)
'Version 3.1.10.251 --> 2021/06/01 : 일일보고서 출력 시 현재페이지 전체 페이지 수 표시
'Version 3.1.10.252 --> 2021/06/07 : 일일보고서 출력 시 전체페이지수 0프로표시되어 수정
'Version 3.1.10.253 --> 2021/06/07 : 일일보고서 출력 시 전체페이지수 0프로표시되어 수정(재배포)
'Version 3.1.10.254 --> 2021/07/26 : 일일보고서 xpert 겹치는 현상 제거 위해 멀티라인 소스 변경
'Version 3.1.10.255 --> 2021/08/18 : 일일보고서 xpert 겹치는 현상 제거 위해 멀티라인 소스 변경 세부참조검사 세부 참조검사 세부 참조목록 등 다 제외되도록 수정
'Version 3.1.10.256 --> 2021/08/23 : 일일보고서 xpert 세부참조 나오는 현상 제거

<Assembly: AssemblyVersion("3.1.10.256")>
