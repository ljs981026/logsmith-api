using Logsmith.Api.Data;
using Logsmith.Api.Domain.Entities;
using Logsmith.Api.Dto;
using Logsmith.Api.Interface;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Logsmith.Api.Service
{
    public class EventService : IEventService
    {
        private readonly LogSmithDbContext _dbContext;

        public EventService(LogSmithDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private async Task<EventResponseDto> CreateAsync(EventCreateDto dto)
        {
            var entity = new EventLog
            {
                UserId = dto.UserId,
                EventType = dto.EvnetType,
                SessionId = dto.SessionId,
                Device = dto.Device,
                Country = dto.Country,
                LatencyMs = dto.LatencyMs,
                // 임의의 오브젝트를 JSON 문자열로 직렬화하여 db에 저장
                Properties = dto.Properties is null ? null : JsonSerializer.Serialize(dto.Properties),
                CreatedDate = DateTime.UtcNow.AddHours(9)
            };

            _dbContext.EventLogs.Add(entity);
            await _dbContext.SaveChangesAsync();

            return new EventResponseDto(
                    entity.Id,
                    entity.UserId,
                    entity.EventType,
                    entity.SessionId,
                    entity.Device,
                    entity.Country,
                    entity.LatencyMs,
                    entity.CreatedDate
                );
        }

        private async Task<IEnumerable<EventResponseDto>> GetListAsync(
                string? userId, string? eventType, DateTime? from, DateTime? to, int page, int pageSize
            )
        {
            // 읽기 전용 쿼리 => AsNoTracking()으로 성능 최적화(변경 추적x)
            var q = _dbContext.EventLogs.AsNoTracking();
            // 선택적 필터(유저, 이벤트 유형, 기간)
            if (!string.IsNullOrWhiteSpace(userId)) q = q.Where(x => x.UserId == userId);
            if (!string.IsNullOrWhiteSpace(eventType)) q = q.Where(x => x.EventType == eventType);
            if (from.HasValue) q = q.Where(x => x.CreatedDate >= from.Value);
            if (to.HasValue) q = q.Where(x => x.CreatedDate < to.Value);
            // 정렬 > 페이징 > dto 프로젝션 > 실행
            return await q
                .OrderByDescending(x => x.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new EventResponseDto(
                    x.Id,
                    x.UserId,
                    x.EventType,
                    x.SessionId,
                    x.Device,
                    x.Country,
                    x.LatencyMs,
                    x.CreatedDate
                )).ToListAsync();
        }

        Task<EventResponseDto> IEventService.CreateAsync(EventCreateDto dto)
        {
            return CreateAsync(dto);
        }

        Task<IEnumerable<EventResponseDto>> IEventService.GetListAsync(string? userId, string? eventType, DateTime? from, DateTime? to, int page, int pageSize)
        {
            return GetListAsync(userId, eventType, from, to, page, pageSize);
        }
    }
}
