using Logsmith.Api.Dto;

namespace Logsmith.Api.Interface
{
    public interface IEventService
    {
        Task<EventResponseDto> CreateAsync(EventCreateDto dto);

        Task<IEnumerable<EventResponseDto>> GetListAsync(string? userId, string? eventType, DateTime? from, DateTime? to, int page, int pageSize);
    }
}
