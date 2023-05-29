# 2달동안 핫소스통 만들기

- API 데이터 포맷은 JSON
  - API구조체는 서버 리포지토리의 위키에서

## 만들어야 하는 씬
- 타이틀 씬
  - 로그인과 회원가입 처리
  - 로그인 성공 시 게임 데이터 초기화 후 메인 화면 씬으로
- 메인 화면 씬
  - 덱 빌딩과 배틀 등으로 넘어갈 수 있는 메인 화면
  - 보유 재화 등 갖고있는 갖고 있음
  - 우편, 출석보상 등을 확인할 수 있는 우편 UI
  - 카드팩 가챠를 구매하고 실시할 수 있는 상점 UI
- 덱 빌딩 씬
  - 덱 리스트를 서버에서 가져와 현재 저장된 리스트 보여주기
  - 새 덱 리스트 추가 응답 요청
  - 덱 수정작업 후 서버에 작업의 valid 요청
  - 덱 삭제작업 후 서버에 작업의 valid 요청
  - 카드 생성 및 삭제에 대해 작업 후 서버에 valid여부 요청
- 배틀 씬
  - 전형적인 하스스톤 방식의 전투가 이뤄지는 곳
  - 웹서버가 아닌 소켓을 이용해 구현할 수 있음

## 단계 나누어 구현해보기

### 1단계 : 서버와의 네트워킹
최종결과물 : 타이틀 씬, 메인 씬
- 네트워크 모듈 및 데이터 매니저 구현
- 회원가입, 로그인, 출석 보상, 메일, 상점 카드깡 등 기본적으로 서버와 통신하는 UI 구현

### 2단계 : 덱 빌딩
최종결과물 : 덱 빌딩 씬
- 덱의 생성, 수정, 삭제 작업을 실시할 수 있는 UI 구현

### 3단계 : 배틀 구현
최종결과물 : 배틀 씬
- 하스스톤 방식의 배틀 구현
- 감정표현 구현

# Wiki
- [데이터 및 처리 규칙](https://github.com/HotSauceTong/Client/wiki/%EC%B2%98%EB%A6%AC%ED%95%B4%EC%95%BC-%ED%95%A0-%EB%8D%B0%EC%9D%B4%ED%84%B0-%EB%B0%8F-%EC%B2%98%EB%A6%AC-%EA%B7%9C%EC%B9%99)
