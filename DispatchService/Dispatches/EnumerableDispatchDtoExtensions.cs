namespace DispatchService.Dispatches;

public static class EnumerableDispatchDtoExtensions
{
    public static DispatchesDto AsDto(this IEnumerable<DispatchDto> dtos) => new(dtos.ToArray());
}
