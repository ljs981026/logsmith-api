namespace Logsmith.Api.Dto;

// 불변객체여서 record로 선언
// 이벤트 생성 객체
public record EventCreateDto
(
    string? UserId,
    string EvnetType,
    string? SessionId,
    string? Device,
    string? Country,
    int? LatencyMs,
    object? Properties
);

// 이벤트 응답 객체
public record EventResponseDto (
    long Id,
    string? UserId,
    string EventType,
    string? SessionId,
    string? Device,
    string? Country,
    int? LatencyMs,
    DateTime CreatedDate
);