namespace Logsmith.Api.Domain.Entities
{
    public class EventLog
    {
        public long Id { get; set; }                        // 기본키
        public string? UserId { get; set; }                 // 유저 아이디
        public string EventType { get; set; } = null!;      // 이벤트 종류
        public string? SessionId { get; set; }              // 세션 식별값
        public string? Device { get; set; }                 // 접속 기기
        public string? Country { get; set; }                // 국가 코드
        public int? LatencyMs { get; set; }                 // 지연시간
        public string? Properties { get; set; }             // 기타 정보 json
        public DateTime CreatedDate { get; set; }           // 발생 시간
    }
}