Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("CDHELP")> 
<Assembly: AssemblyDescription("MEDI@CK.NET LIS 프로그램")> 
<Assembly: AssemblyCompany("ACK Co.,Ltd.")> 
<Assembly: AssemblyProduct("MEDI@CK .Net v3")> 
<Assembly: AssemblyCopyright("Copyrightⓒ 2010 ACK Co.,Ltd. All rights reserved")> 
<Assembly: AssemblyTrademark("MEDI@CK")> 
<Assembly: ComVisible(False)> 

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("3ec08453-b567-4452-9c2b-e2a6612b3438")> 
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
'Version 3.1.10.202 --> 2013/02/12 : 보관검체 쿼리 일짜 형식 수정 (3일만 가져오게)
'Version 3.1.10.313 --> 2013/06/20 : DB 접속 변경
'Version 3.1.10.314 --> 2014/01/07 : EMR에서 코드 조회가능기능 추가 
'Version 3.1.10.315 --> 2016/04/27 : EMR에서 코드 조회 수정  
'Version 3.1.10.316 --> 2018/02/27 : vs2010 upgarde
'Version 3.1.10.317 --> 2019/05/17 : 검사정보관리-검사의뢰정보 디자인 추가
'Version 3.1.10.318 --> 2019/05/17 : 검사정보관리-검사의뢰정보 디자인 추가(재조정)
'Version 3.1.10.320 --> 2019/07/05 : 검사정보관리 - 디자인 변경(데모버전)
'Version 3.1.10.321 --> 2019/07/08 : 검사정보관리 - 디자인 변경(데모버전)
'Version 3.1.10.322 --> 2019/08/20 : 검사정보관리 - 디자인 변경(데모버전)
'Version 3.1.10.323 --> 2019/08/20 : 검사정보관리 - 디자인 변경(데모버전)
'Version 3.1.10.324 --> 2020/08/06 : 검사의뢰지침 수정
'Version 3.1.10.325 --> 2020/09/03 : 검사의뢰지침 수정
'Version 3.1.10.326 --> 2020/10/12 : 검사의뢰지침 수정
'Version 3.1.10.327 --> 2020/11/30 : 검사의뢰지침 세부검사 수정
'Version 3.1.10.328 --> 2020/12/29 : 검사의뢰지침 돋보기 기능 다시 클릭시 되지 않아 수정 
'Version 3.1.10.329 --> 2020/12/29 : 검사의뢰지침 돋보기 기능 다시 클릭시 되지 않아 수정 (재배포)
'Version 3.1.10.330 --> 2021/01/11 : 검사의뢰지침 돋보기 오류 수정
'Version 3.1.10.331 --> 2021/04/05 : 검사의뢰지침 돋보기 오류 수정
'Version 3.1.10.332 --> 2021/04/05 : 검사의뢰지침 돋보기 오류 수정(재배포)
'Version 3.1.10.333 --> 2021/05/17 : 검사의뢰지침 결과소요일 컬럼 추가 

<Assembly: AssemblyVersion("3.1.10.333")> 