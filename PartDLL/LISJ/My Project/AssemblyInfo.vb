Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("LISJ")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("ACK")> 
<Assembly: AssemblyProduct("LISJ")> 
<Assembly: AssemblyCopyright("Copyright © ACK 2011")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)> 

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("90492b35-a2ec-4b81-906a-593ffe864a84")> 

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
'Version 3.1.10.213 --> 2012/04/10 : 워크리스트 화면에서 검사항목 아이템 저장 시 에러사항 수정
'Version 3.1.10.214 --> 2012/11/07 : 위탁검사리스트작성시 처방의컬럼 추가(LISAPP , LISJ)
'Version 3.1.10.215 --> 2012/12/10 : 바코드재출력 초기 셋팅 검체번호로 변경 (LISJ)
'Version 3.1.10.216 --> 2013/02/07 : 검체전달 사원번호 입력 readonly 해제 (LISJ)
'Version 3.1.10.313 --> 2013/06/20 : DB 접속 변경
'Version 3.1.10.333 --> 2013/08/07 : W/L 화면에서 베터리 코드 조회 가능하도록 수정
'Version 3.1.10.334 --> 2014/07/04 : 바코드 재출력시 감염정보 표시 수정 
'Version 3.1.10.336 --> 2016/02/25 : 바코드 재출력화면에서 채혈시 주의사항 컬럼 추가(주의사항 있을시 작업번호 주황색으로 색깔처리)
'Version 3.1.10.337 --> 2016/02/25 : 재배포
'Version 3.1.10.338 --> 2016/02/25 : 재배포
'Version 3.1.10.339 --> 2016/05/13 : Brain접수 추가
'Version 3.1.10.340 --> 2016/05/13 : Brain접수 추가(개별접수 추가)
'Version 3.1.10.341 --> 2016/05/24 : Brain접수 추가(개별접수 추가) - 재배포
'Version 3.1.10.342 --> 2018/02/27 : vs2010
'Version 3.1.10.343 --> 2018/02/27 : 위탁검사 SCL 모드 추가 
'Version 3.1.10.346 --> 2018/04/03 : 위탁검사 SCL 접수시 주민번호 오류 수정
'Version 3.1.10.347 --> 2018/04/25 : 환자 혈액형 여부 바코드 표시
'Version 3.1.10.348 --> 2019/08/28 : 위탁리스트작성 조회기간 시간까지
'Version 3.1.10.349 --> 2019/09/03 : 위탁검사(삼광) 체크한것만 DB들어가도록 수정
'Version 3.1.10.350 --> 2020/02/27 : 특수보고서 코로나 결과값 접수취소,reject 진행시 lrs17m 데이터 삭제 추가
'Version 3.1.10.351 --> 2020/06/23 : 채혈접수취소, 채혈취소, Reject할때 자체응급 삭제 추가
'Version 3.1.10.352 --> 2020/06/30 : 위탁검사 녹십자 추가
'Version 3.1.10.353 --> 2020/11/23 : 바코드 재출력시 자체응급 추가
'Version 3.1.10.354 --> 2021/01/04 : 위탁검사리스트 채혈시 주의사항 삽입
'Version 3.1.10.355 --> 2021/02/22 : 배지바코드 5장 출력 될 수 있도록 구현

<Assembly: AssemblyVersion("3.1.10.355")> 

