Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' 어셈블리의 일반 정보는 다음 특성 집합을 통해 제어됩니다.
' 어셈블리와 관련된 정보를 수정하려면
' 이 특성 값을 변경하십시오.

' 어셈블리 특성 값을 검토합니다.

<Assembly: AssemblyTitle("Main")> 
<Assembly: AssemblyDescription("MEDI@CK.NET LIS 프로그램")> 
<Assembly: AssemblyCompany("ACK Co.,Ltd.")> 
<Assembly: AssemblyProduct("MEDI@CK .Net v3")> 
<Assembly: AssemblyCopyright("Copyrightⓒ 2010 ACK Co.,Ltd. All rights reserved")> 
<Assembly: AssemblyTrademark("MEDI@CK")> 
<Assembly: ComVisible(False)> 

'이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.
<Assembly: Guid("56056c38-264b-4505-8df2-597ec2d38dee")>
'<Assembly: AssemblyFileVersion("1.0.0.0")> 

' 어셈블리의 버전 정보는 다음 네 가지 값으로 구성됩니다.
'
'      주 버전
'      부 버전
'      빌드 번호
'      수정 버전
'
' 모든 값을 지정하거나 아래와 같이 '*'를 사용하여 빌드 번호 및 수정 버전이 자동으로
' 지정되도록 할 수 있습니다.s
' <Assembly: AssemblyVersion("1.0.*")> 
'Version 3.1.10.416 --> 2015/05/12 : 기초마스터 배양균속 항균제 조회 쿼리 수정
'Version 3.1.10.417 --> 2015/05/12 : 배포문제로 재배포
'Version 3.1.10.419 --> 2015/05/14 : 추가처방 TEST완료후 배포
'Version 3.1.10.420 --> 2015/05/14 : 추가처방 TEST완료후 배포(LISM 배포가 제대로 이루어지지않음)
'Version 3.1.10.421 --> 2015/05/14 : 추가처방 TEST완료후 배포(LISM 배포가 제대로 이루어지지않음)2
'Version 3.1.10.422 --> 2015/05/14 : 추가처방 TEST완료후 배포(LISM 배포가 제대로 이루어지지않음)3
'Version 3.1.10.423 --> 2015/05/14 : 추가처방 TEST완료후 배포(LISM 배포가 제대로 이루어지지않음)4
'Version 3.1.10.426 --> 2015/06/04 : SATO400/루칸바코드 감염정보 표시 재변경(폰트변경) 및 바코드 출력일시 표시 위치 변경(우측 상단->우측 하단), 상호 수정
'Version 3.1.10.427 --> 2015/08/18 : LISAPP , LISR ,LISM
'Version 3.1.10.428 --> 2015/08/18 : main, LISM (배포오류)
'Version 3.1.10.430 --> 2015/08/18 : main, LISAPP (배포오류)
'Version 3.1.10.431 --> 2015/08/27 : main, LISM (추가처방 오류)
'Version 3.1.10.432 --> 2015/08/31 : main, LISM (추가처방 오류)
'Version 3.1.10.433 --> 2015/09/23 : MAIN, LISM , LISR , LISAPP 
'Version 3.1.10.434 --> 2015/09/23 : MAIN, LISAPP 
'Version 3.1.10.435 --> 2015/09/25 : MAIN, LISAPP 
'Version 3.1.10.436 --> 2015/10/12 : 검사항목별 결과관리 스프레드 사이즈 조절.
'Version 3.1.10.439 --> 2015/10/16 : 오픈카드 수정 
'Version 3.1.10.440 --> 2015/10/27 : 오픈카드 수정 
'Version 3.1.10.441 --> 2015/10/27 : MAIN, LISAPP, LISB 
'Version 3.1.10.442 --> 2015/12/17 : MAIN, AxAckResultViewer
'Version 3.1.10.443 --> 2015/12/17 : MAIN, AxAckResultViewer(배포버전오류)
'Version 3.1.10.444 --> 2016/1/21 : 누적결과 최근 결과가 앞으로 오게 수정
'Version 3.1.10.445 --> 2016/01/28 : 채혈 접수 조회 화면 검사항목 SINGLE 코드 추가.
'Version 3.1.10.446 --> 2016/01/28 : 채혈 접수 조회 화면 검사항목 SINGLE 코드 추가. -배포오류
'Version 3.1.10.447 --> 2016/02/02 : 질병관리 본부 조회 오류 배포
'Version 3.1.10.448 --> 2016/02/02 : 가출고 화면 디자인 깨져 재배포
'Version 3.1.10.449 --> 2016/02/02 : 수술환자 확정 조회 혈액형 안나와 수정.
'Version 3.1.10.450 --> 2016/02/25 : LISB , LISR , LIST , LISAPP ,LISJ
'Version 3.1.10.451 --> 2016/02/25 : 재배포
'Version 3.1.10.452 --> 2016/02/25 : 재배포
'Version 3.1.10.453 --> 2016/02/25 : 재배포
'Version 3.1.10.455 --> 2016/04/05 : LISS ,LISAPP
'Version 3.1.10.456 --> 2016/04/05 : LISS ,LISAPP
'Version 3.1.10.457 --> 2016/04/26 : LISS ,LISAPP
'Version 3.1.10.458 --> 2016/05/04 : 병원체 검사결과 신고 hidden된 bcno컬럼 제외하고 엑셀파일 생성(LISS)
'Version 3.1.10.459 --> 2016/05/04 : 병원체 검사결과 신고 hidden된 컬럼이 있으면 컬럼 삭제 후 엑셀 파일 생성 (LISS)
'Version 3.1.10.500 --> 2016/05/04 : 병원체 검사결과 신고 hidden된 컬럼이 있으면 컬럼 삭제 후 엑셀 파일 생성 (LISS)-버그수정
'Version 3.1.10.501 --> 2016/05/13 : Brain접수 추가, 혈액은행 자체폐기/교환 수정 (LISR, LISAPP,LISJ,LISB)
'Version 3.1.10.502 --> 2016/05/19 : 검사통계 조회 수정(LISAPP), Brain접수 개별접수 추가(LISJ)
'Version 3.1.10.503 --> 2016/05/24 : 검체통계조회  추가(LIST)
'Version 3.1.10.504 --> 2016/05/24 : Brain접수 재배포
'Version 3.1.10.505 --> 2016/05/26 : R01
'Version 3.1.10.506 --> 2016/05/27 : LISAPP
'Version 3.1.10.507 --> 2016/06/17 : 자동소견 두개 붙을경우 한칸 띄기 LISAPP
'Version 3.1.10.508 --> 2016/06/17 : 자동소견 두개 붙을경우 한칸 띄기 LISAPP
'Version 3.1.10.509 --> 2016/06/21 : OCSAPP
'Version 3.1.10.510 --> 2016/07/06 : LIST
'Version 3.1.10.511 --> 2016/07/21 : LISS , LISAPP
'Version 3.1.10.512 --> 2016/08/03 : LISf , AxAckResultViwer
'Version 3.1.10.516 --> 2016/08/31 : POPUPWIN(메르스, 지카바이러스 모듈 추가)
'Version 3.1.10.517 --> 2016/09/05 : POPUPWIN(메르스, 지카바이러스 모듈 추가)(재배포)
'Version 3.1.10.518 --> 2016/09/22 : BCPRT01 
'Version 3.1.10.519 --> 2016/10/07 : 결과조회 멀티라인 결과로 출력 될 수 있도록 수정 
'Version 3.1.10.520 --> 2016/10/07 : 결과조회 멀티라인 결과로 출력 될 수 있도록 수정 (재배포)
'Version 3.1.10.521 --> 2016/10/07 : 결과조회 멀티라인 결과로 출력 될 수 있도록 수정 (재배포)
'Version 3.1.10.522 --> 2016/10/20 : BCPRT01 - SATO400 미채혈 바코드 글자 크기 변경, LISS, LISAPP - 채혈 및 접수대장 미접수 바코드 조회조건 추가
'Version 3.1.10.524 --> 2016/10/20 : BCPRT01 - SATO400 미채혈 바코드 글자 크기 변경, LISS, LISAPP - 채혈 및 접수대장 미접수 바코드 조회조건 추가
'Version 3.1.10.528 --> 2016/11/22 : 결핵검사 진행 시 환자의 CBC검사항목 결과 소견으로 가져오도록 수정.
'Version 3.1.10.529 --> 2016/12/20 : 검체별 결과저장 parent 단독 보고 시 검증 안되는 현상 해결.
'Version 3.1.10.530 --> 2016/12/23 : 혈액 반납/폐기율 조회 오류 수정
'Version 3.1.10.531 --> 2016/12/23 : 혈액 반납/폐기율 조회 오류 수정
'Version 3.1.10.532 --> 2016/12/26 : 채혈 및 접수대장 부서 조회 조건 추가
'Version 3.1.10.533 --> 2016/12/26 : 채혈 및 접수대장 부서 조회 조건 추가 재배포
'Version 3.1.10.534 --> 2017/01/02 : 미생물 추가 처방 시 확인의 변경
'Version 3.1.10.535 --> 2017/01/02 : 결과 조회 출력지 내용 변경
'Version 3.1.10.536 --> 2017/01/18 : TAT통계 유효건수, 전체건수 안맞는 부분 수정.
'Version 3.1.10.537 --> 2017/01/26 : LISAPP , LISS
'Version 3.1.10.539 --> 2017/02/23 : LIST , POPUPWIN
'Version 3.1.10.540 --> 2017/03/02 : LISM 
'Version 3.1.10.541 --> 2017/03/03 : LISM 
'Version 3.1.10.542 --> 2017/03/04 : POPUPWIN 
'Version 3.1.10.544 --> 2017/03/23 : LIST
'Version 3.1.10.545 --> 2017/03/24 : LISS
'Version 3.1.10.546 --> 2017/03/27 : LISS
'Version 3.1.10.548 --> 2017/04/10 : LISAPP , COMMON , LISM
'Version 3.1.10.549 --> 2017/04/10 : AckAxResult
'Version 3.1.10.550 --> 2017/04/14 : LISAPP
'Version 3.1.10.551 --> 2017/04/14 : LIST
'Version 3.1.10.554 --> 2017/05/11 : LISS
'Version 3.1.10.555 --> 2017/05/15 : LISAPP
'Version 3.1.10.556 --> 2017/05/18 : LISO
'Version 3.1.10.557 --> 2017/05/30 : LISO , LIST , LISS , LISAPP , AckAxResult 
'Version 3.1.10.558 --> 2017/06/05 : LIST , LISAPP 
'Version 3.1.10.559 --> 2017/06/21 : LISB , LISAPP , LISF
'Version 3.1.10.560 --> 2017/06/23 : LISF
'Version 3.1.10.561 --> 2017/07/06 : AckAxResult
'Version 3.1.10.562 --> 2017/07/13 : LIST , LISM , LISAPP
'Version 3.1.10.563 --> 2017/07/17 : LISAPP , AXACKCOLLECTOR ,CO1
'Version 3.1.10.564 --> 2017/07/17 : f01
'Version 3.1.10.566 --> 2017/07/18 : AXACKCOLLECTOR
'Version 3.1.10.567 --> 2017/08/17 : LISAPP, COMMON , WEBSERVER , LISF , LISS , LISB
'Version 3.1.10.568 --> 2017/08/29 : LISAPP
'Version 3.1.10.569 --> 2017/08/29 : LISAPP ,LISS
'Version 3.1.10.570 --> 2017/09/04 : LISAPP , COMMON , WEBSERVER ,  LISS , LISF
'Version 3.1.10.571 --> 2017/09/05 : LISS , POPUPWIN
'Version 3.1.10.572 --> 2017/09/06 : LISS 
'Version 3.1.10.573 --> 2017/09/12 : LISS ,LISAPP , LISF
'Version 3.1.10.574 --> 2017/09/12 : LISAPP 
'Version 3.1.10.575 --> 2017/09/19 : LISAPP
'Version 3.1.10.576 --> 2017/09/22 : LISAPP , AXACKCOLLECTOR , LISF , LISV 
'Version 3.1.10.577 --> 2017/09/23 : LISM , LISV 
'Version 3.1.10.578 --> 2017/10/25 : BCPRT01, LISS , LISAPP , COMMON
'Version 3.1.10.579 --> 2017/10/25 : BCPRT01
'Version 3.1.10.580 --> 2017/10/30 : BCPRT01
'Version 3.1.10.581 --> 2017/10/30 : BCPRT01
'Version 3.1.10.582 --> 2017/10/30 : BCPRT01
'Version 3.1.10.583 --> 2017/11/01 : LISS
'Version 3.1.10.584 --> 2017/12/04 : LISS , LISAPP 
'Version 3.1.10.585 --> 2018/01/10 : AxAckPatientInfo , OCSAPP
'Version 3.1.10.586 --> 2018/01/11 : LISS
'Version 3.1.10.587 --> 2018/01/11 : LisAPP
'Version 3.1.10.588 --> 2018/01/12 : LisAPP , LISB
'Version 3.1.10.589 --> 2018/01/16 : LisAPP 
'Version 3.1.10.590 --> 2018/01/17 : LisAPP 
'Version 3.1.10.591 --> 2018/01/18 : LisAPP 
'Version 3.1.10.592 --> 2018/01/18 : LIS0
'Version 3.1.10.593 --> 2018/01/23 : LisAPP
'Version 3.1.10.594 --> 2018/01/25 : LisAPP , LISB
'Version 3.1.10.595 --> 2018/01/29 : LisAPP 
'Version 3.1.10.596 --> 2018/02/06 : bcprt01
'Version 3.1.10.597 --> 2018/02/08 : LISAPP , OCSAPP
'Version 3.1.10.598 --> 2018/02/13 : LISAPP , LISF , LISS
'Version 3.1.10.599 --> 2018/02/20 : AXACKRESUTVIEWER
'Version 3.1.10.600 --> 2018/02/21 : POPUPWIN
'Version 3.1.10.601 --> 2018/02/27 : vs2010 dll 전체
'Version 3.1.10.602 --> 2018/03/02 : AxAckPatientInfo
'Version 3.1.10.603 --> 2018/03/06 : LISAPP, LISS
'Version 3.1.10.604 --> 2018/03/30 : LISR, LISJ ,LISAPP
'Version 3.1.10.605 --> 2018/03/30 :  LISJ ,LISAPP
'Version 3.1.10.606 --> 2018/03/30 :  LISJ ,LISAPP , LISR , DBSSERVER , COMMON
'Version 3.1.10.607 --> 2018/04/03 :  LISJ ,LISAPP , LISR 
'Version 3.1.10.608 --> 2018/04/03 :  LISS 
'Version 3.1.10.610 --> 2018/04/03 :  AXACKRESULT
'Version 3.1.10.611 --> 2018/04/16 :  LISR
'Version 3.1.10.671 --> 2019/08/20 :  LISR , cdhelp, bcprt01, lisf , lisc, axackcollector , lisapp , webserver
'Version 3.1.10.672 --> 2019/08/20 :   cdhelp
'Version 3.1.10.673 --> 2019/08/26 : WEBSERVER
'Version 3.1.10.674 --> 2019/08/28 : LISJ, Axackresult
'Version 3.1.10.674 --> 2019/08/28 : LISJ, Axackresult
'Version 3.1.10.675 --> 2019/09/03 : LISJ
'Version 3.1.10.676 --> 2019/10/10 : LISV, LISAPP
'Version 3.1.10.677 --> 2019/10/10 : LISV, LISAPP
'Version 3.1.10.678 --> 2019/10/16 : WEBSERVER
'Version 3.1.10.679 --> 2019/11/08 : LISS
'Version 3.1.10.680 --> 2019/11/20 : lisapp, lisr , axackresult , axackpatientinfo , axackcollector, lisb , bcprt01 , axackrichtextbox , list , common 
'Version 3.1.10.682 --> 2019/12/30 : LISAPP, AxAckResult
'Version 3.1.10.683 --> 2019/12/30 : LISF
'Version 3.1.10.684 --> 2019/12/31 : AxAckResult
'Version 3.1.10.685 --> 2019/12/31 : AxAckResult
'Version 3.1.10.686 --> 2020/01/09 : LISAPP, AxAckResult, LISM
'Version 3.1.10.687 --> 2020/02/27 : LISAPP, COMMON, POPUPWIN, LISJ, LISR
'Version 3.1.10.688 --> 2020/03/06 : LISAPP, POPUPWIN, LISF
'Version 3.1.10.689 --> 2020/03/17 : LISAPP, POPUPWIN, LISF
'Version 3.1.10.690 --> 2020/03/30 : LISS, LISB
'Version 3.1.10.691 --> 2020/04/01 : LISAPP
'Version 3.1.10.692 --> 2020/04/13 : LISC, LISS
'Version 3.1.10.693 --> 2020/04/23 : AxAckCollector, AxAckPatientinfo, AxAckResult, OCSAPP, COMMON, LISB
'Version 3.1.10.694 --> 2020/04/25 : LISF
'Version 3.1.10.695 --> 2020/05/18 : POPUPWIN
'Version 3.1.10.696 --> 2020/05/25 : AxAckCollector, AxAckPatientinfo, AxAckResult, OCSAPP, COMMON, LISB
'Version 3.1.10.697 --> 2020/05/27 : LISAPP, POPUPWIN
'Version 3.1.10.698 --> 2020/05/28 : POPUPWIN
'Version 3.1.10.699 --> 2020/05/29 : Main
'Version 3.1.10.700 --> 2020/06/02 : LISAPP, OCSAPP, AxAckResult, LISR
'Version 3.1.10.701 --> 2020/06/03 : LISAPP, AxAckResult
'Version 3.1.10.702 --> 2020/06/03 : AxAckResult
'Version 3.1.10.703 --> 2020/06/03 : AxAckResult
'Version 3.1.10.704 --> 2020/06/23 : LISAPP, LIST, AxAckCollector, LISJ, AxAckResult, COMMON
'Version 3.1.10.705 --> 2020/06/30 : AxAckResult, COMMON, LISAPP, DBORA, LISJ, LISR
'Version 3.1.10.706 --> 2020/07/02 : COMMON, LISAPP, LISR
'Version 3.1.10.707 --> 2020/07/06 : COMMON, LISS
'Version 3.1.10.708 --> 2020/07/09 : LISS
'Version 3.1.10.709 --> 2020/07/09 : LISS, COMMON
'Version 3.1.10.710 --> 2020/08/04 : AxAckResult, LISAPP, OCSAPP, PRTAPP, LISS, LIST, LISB, LISF
'Version 3.1.10.711 --> 2020/08/04 : POPUPWIN, LISR
'Version 3.1.10.712 --> 2020/08/05 : LISAPP, BCPRPT01, LISM
'Version 3.1.10.713 --> 2020/08/05 : POPUPWIN
'Version 3.1.10.714 --> 2020/08/05 : LISM
'Version 3.1.10.715 --> 2020/08/06 : AxAckCollector, CDHELP, LISF, LIST, LISR
'Version 3.1.10.716 --> 2020/08/10 : Main(자동로그아웃 버튼 위치조정)
'Version 3.1.10.717 --> 2020/08/10 : LISM
'Version 3.1.10.718 --> 2020/08/24 : POPUPWIN
'Version 3.1.10.719 --> 2020/09/03 : CDHELP, LISAPP, LISF
'Version 3.1.10.720 --> 2020/09/24 : LISAPP, LISS
'Version 3.1.10.721 --> 2020/10/08 : POPUPWIN
'Version 3.1.10.722 --> 2020/10/08 : POPUPWIN
'Version 3.1.10.723 --> 2020/10/12 : CDHELP, LISF, POPUPWIN
'Version 3.1.10.724 --> 2020/10/13 : POPUPWIN
'Version 3.1.10.725 --> 2020/10/20 : AxAckRichTextBox
'Version 3.1.10.726 --> 2020/10/22 : POPUPWIN
'Version 3.1.10.727 --> 2020/11/23 : BCPRT01, PRTAPP, AxAckRichTextBox, LISAPP, OCSAPP, LISB, LISJ
'Version 3.1.10.728 --> 2020/11/24 : AxAckPatienInfo, LISAPP, LISB
'Version 3.1.10.729 --> 2020/11/30 : LISF, LISAPP, COMMON, CDHLEP
'Version 3.1.10.730 --> 2020/12/01 : PRTAPP
'Version 3.1.10.731 --> 2020/12/05 : AxAckResult
'Version 3.1.10.732 --> 2020/12/10 : BCPRPT01
'Version 3.1.10.733 --> 2020/12/10 : AxAckResult
'Version 3.1.10.734 --> 2020/12/11 : AxAckResult
'Version 3.1.10.735 --> 2020/12/21 : LISB
'Version 3.1.10.736 --> 2020/12/28 : CDHELP
'Version 3.1.10.737 --> 2020/12/29 : CDHELP(재배포)
'Version 3.1.10.738 --> 2021/01/04 : LISJ, LISAPP
'Version 3.1.10.739 --> 2021/01/06 : LISB, LISAPP
'Version 3.1.10.740 --> 2021/01/07 : LISB
'Version 3.1.10.741 --> 2021/01/08 : LISAPPP
'Version 3.1.10.742 --> 2021/01/11 : LISB, CDHELP
'Version 3.1.10.743 --> 2021/01/25 : LISB
'Version 3.1.10.744 --> 2021/01/27 : BCPRT01
'Version 3.1.10.745 --> 2021/02/15 : LISS,LISAPPP
'Version 3.1.10.746 --> 2021/02/22 : LISS,LISJ,
'Version 3.1.10.747 --> 2021/02/24 : LISAPP
'Version 3.1.10.748 --> 2021/03/11 : AxAckResultViewer
'Version 3.1.10.749 --> 2021/03/15 : AxAckResultViewer
'Version 3.1.10.750 --> 2021/03/15 : AxAckResultViewer
'Version 3.1.10.751 --> 2021/03/16 : AxAckResult,LISR,LISB,COMMON, LISAPP,
'Version 3.1.10.752 --> 2021/03/16 : AxAckResult
'Version 3.1.10.753 --> 2021/03/20 : LISC
'Version 3.1.10.754 --> 2021/03/22 : LISC, LISB, LISM
'Version 3.1.10.755 --> 2021/04/05 : CDHELP
'Version 3.1.10.756 --> 2021/04/05 : CDHELP(재배포)
'Version 3.1.10.757 --> 2021/04/19 : LISS,LISB,LISR,COMMON,LISAPP,OCSAPP
'Version 3.1.10.758 --> 2021/04/20 : LISR
'Version 3.1.10.759 --> 2021/04/20 : LISR
'Version 3.1.10.760 --> 2021/05/17 : AxAckResult, LISAPP, CDHELP, LISB, LISR, LISS
'Version 3.1.10.761 --> 2021/05/21 : LISAPP, LISS
'Version 3.1.10.762 --> 2021/05/31 : LISAPP, LISR
'Version 3.1.10.763 --> 2021/06/01 : AxAckResultViewer
'Version 3.1.10.764 --> 2021/06/07 : DBORA, AxAckResultViewer
'Version 3.1.10.765 --> 2021/06/07 : DBORA, AxAckResultViewer
'Version 3.1.10.766 --> 2021/06/15 : LISAPP, LISC, LISJ, AXACKCOLLECTOR, COMMON, BCPRPT01, PRTAPP
'Version 3.1.10.767 --> 2021/06/22 : CDHELP, LISS, LISAPP, COMMON, AxAckPatientInfo
'Version 3.1.10.768 --> 2021/06/25 : CDHELP, LISM, LISR, LISAPP , COMMON, AxAckPatientInfo, COMMON
'Version 3.1.10.769 --> 2021/06/28 : LISM, LISR
'Version 3.1.10.770 --> 2021/07/19 : AxAckResult, LISAPP, COMMON, LISF
'Version 3.1.10.771 --> 2021/07/26 : AxAckPatinfo, AxAckResultViewer, LISS, LISR, LISAPP, LISB, BCPRT01
'Version 3.1.10.772 --> 2021/07/26 : COMMON
'Version 3.1.10.773 --> 2021/07/26 : BCPRT01
'Version 3.1.10.774 --> 2021/08/02 : AxAckResult, LISR, LISM
'Version 3.1.10.775 --> 2021/08/02 : AxAckResult, LISR, LISM(재배포)
'Version 3.1.10.776 --> 2021/08/02 : AxAckResult(재배포)
'Version 3.1.10.777 --> 2021/08/18 : LISF. LISS. COMMON, CDHELP, LISAPP, AXACKRESULT, AXACKRESULTVIEWER
'Version 3.1.10.778 --> 2021/08/23 : LISS, LISAPP, AXACKRESULTVIEWER
'Version 3.1.10.779 --> 2021/09/01 : LISF, LISS, LIAPP, COMMON,AxAckResult
'Version 3.1.10.780 --> 2021/09/02 : LISS, AxAckResult
'Version 3.1.10.781 --> 2021/09/03 : LISS, LISAPP , AxAckResult
'Version 3.1.10.782 --> 2021/09/08 : LISF, AxAckResult
'Version 3.1.10.783 --> 2021/10/18 : LISJ, AxAckResult, LISAPP
'Version 3.1.10.784 --> 2021/10/25 : LISB
'Version 3.1.10.785 --> 2021/10/25 : LISB
'Version 3.1.10.786 --> 2021/10/25 : LISB
'Version 3.1.10.787 --> 2021/11/09 : LISC, AxAckCollector, Common, LISAPP, BCPRT01, PRTAPP,
'Version 3.1.10.788 --> 2021/11/09 : main(재배포)
'Version 3.1.10.789 --> 2021/11/11 : LISAPP
'Version 3.1.10.790 --> 2021/11/22 : CDHELP, LISB , LISAPP
'Version 3.1.10.791 --> 2021/11/29 : LISF, CDHELP, LISB , LISAPP
'Version 3.1.10.792 --> 2021/11/29 : LCDHELP, LISB , LISAPP
'Version 3.1.10.793 --> 2021/11/29 : LISB
'Version 3.1.10.794 --> 2021/12/20 : LISB, LISAPP, LISF, CDHELP
'Version 3.1.10.795 --> 2022/01/17 : LISR, LISAPP, LISB, LISS, AxAckresult
'Version 3.1.10.796 --> 2022/01/17 : AxAckresult
'Version 3.1.10.797 --> 2022/01/20 : LIST
'Version 3.1.10.798 --> 2022/02/08 : Common, LISB, LISAPP, AxAckResult, LISF
'Version 3.1.10.799 --> 2022/02/09 : AxAckResult
'Version 3.1.10.800 --> 2022/02/09 : AxAckResult
'Version 3.1.10.801 --> 2022/02/21 : POPUPWIN
'Version 3.1.10.802 --> 2022/02/23 : AxAckResult, LISAPP, LISB
'Version 3.1.10.803 --> 2022/03/10 : POPUPPWIN
'Version 3.1.10.804 --> 2022/03/14 : POPUPPWIN
'Version 3.1.10.805 --> 2022/03/15 : LISAPP
'Version 3.1.10.806 --> 2022/03/24 : AxAckResult
'Version 3.1.10.807 --> 2022/03/24 : AxAckResult
'Version 3.1.10.808 --> 2022/03/24 : AxAckResult
'Version 3.1.10.809 --> 2022/03/30 : LISAPP, LISB
'Version 3.1.10.810 --> 2022/03/31 : AxAckResult
'Version 3.1.10.811 --> 2022/04/19 : POPUPPWIN
'Version 3.1.10.812 --> 2022/04/25 : LISAPP, LISB
'Version 3.1.10.813 --> 2022/04/26 : LISAPP, LISB
'Version 3.1.10.814 --> 2022/04/26 : LISF
'Version 3.1.10.815 --> 2022/05/18 : DEP TEST
'Version 3.1.10.816 --> 2022/05/25 : LISAPP, LISB
'Version 3.1.10.817 --> 2022/06/22 : AxAckResult
'Version 3.1.10.818 --> 2022/06/23 : AxAckResult
'Version 3.1.10.819 --> 2022/06/24 : AxAckResult
'Version 3.1.10.820 --> 2022/06/24 : AxAckResult
'Version 3.1.10.821 --> 2022/06/27 : AxAckResult
'Version 3.1.10.822 --> 2022/06/28 : LISAPP, LISB, LISF
'Version 3.1.10.823 --> 2022/06/28 : LISB
'Version 3.1.10.824 --> 2022/07/04 : LISAPP
'Version 3.1.10.825 --> 2022/07/04 : COMMON, LISF
'Version 3.1.10.826 --> 2022/07/05 : LISAPP

<Assembly: AssemblyVersion("3.1.10.826")>





