Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("LOGIN")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("ACK")> 
<Assembly: AssemblyProduct("LOGIN")> 
<Assembly: AssemblyCopyright("Copyright © ACK 2011")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)>

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("81720664-dcac-4010-ad9a-bc4a95380913")> 

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
'Version 3.1.10.214 --> 2012/04/12 : LJH 사용자 암호 암호화(제공된 해쉬함수 사용)
'Version 3.1.10.313 --> 2013/06/20 : DB 접속 변경
'Version 3.1.10.342 --> 2013/07/29 : mig 접속 변경
'Version 3.1.10.356 --> 2013/09/02 : 유저 비밀번호 병원 정책 반영
'Version 3.1.10.357 --> 2013/11/01 : 개발서버(구 mig11g) 아이피변경
'Version 3.1.10.358 --> 2018/02/27 : vs2010
<Assembly: AssemblyVersion("3.1.10.358")> 

