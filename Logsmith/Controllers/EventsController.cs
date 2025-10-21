using Microsoft.AspNetCore.Mvc;

namespace Logsmith.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        // 클라이언트가 보내는 이벤트 데이터의 형태
        public record EventDto (
            string? UserId,             // 유저 아이디                nullable
            string EventType,           // 이벤트 종류 (필수값)  
            string? SessionId,          // 세션 식별값                nullable
            string? Device,             // 접속 기기                  nullable
            string? Country,            // 국가 코드                  nullable
            int? LatencyMs,             // 지연시간                   nullable
            object? Properties,         // 기타 정보 json으로 받음    nullable
            DateTime? CreatedDate       // 발생 시간                  nullable
        );

        [HttpPost]
        // IActionResult 다양한 응답 형식을 반환할 수 있게해주는 타입
        public IActionResult Ingest([FromBody] EventDto req)
        {
            // with record 타입에서 값을 복사해서 일부만 변경
            // 요청에 CreatedDate가 null이면 현재 시간으로 설정
            var saved = req with { CreatedDate = req.CreatedDate ?? DateTime.UtcNow.AddHours(9) };
            // 클라이언트에게 201 응답 사용
            // 첫 번째 인자 생성된 리소스의 URI-> /api/events/echo
            // 두 번째 인자: 응답 바디에 담을 데이터
            return Created("/api/events/echo", saved);
            /*
            POST /api/events
            {
                "userId": "u123",
                "eventType": "page_view"
            }

            201 Created
            {
                "userId": "u123",
                "eventType": "page_view",
                "createdDate: "2025-10-21T14:00:00Z"
            }
            */
        }
    }
}
