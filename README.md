# Notificator

## 프로젝트 개요

Notificator는 Chzzk(네이버 게임 스트리밍 플랫폼) 스트리머의 방송 시작을 모니터링하고 Discord를 통해 알림을 보내는 서비스입니다.

## 주요 기능

- 팔로우한 스트리머의 라이브 방송 상태 모니터링
- 스트리머가 방송을 시작할 때 Discord 웹훅을 통한 알림 발송
- 사용자별 맞춤 알림 설정

## 기술 스택

- .NET Core
- ASP.NET Core
- C#
- Discord Webhook API
- Chzzk API

## 설치 및 실행 방법

1. 저장소 클론

```bash
git clone [저장소 URL]
```

2. 프로젝트 디렉토리로 이동

```bash
cd Notificator
```

3. 의존성 설치 및 프로젝트 실행

```bash
dotnet restore
dotnet run
```

## 설정

`appsettings.json` 파일을 통해 다음 설정을 구성할 수 있습니다:

- Discord 웹훅 URL
- 모니터링 주기
- API 키 및 기타 설정

## 아키텍처

- `Service`: 백그라운드 서비스 및 주요 비즈니스 로직
- `ApiSend`: 외부 API 통신 관련 클래스
- `Repository`: 데이터 접근 계층
- `Models`: 데이터 모델 클래스
- `Controllers`: API 컨트롤러

## 라이센스

[라이센스 정보]
