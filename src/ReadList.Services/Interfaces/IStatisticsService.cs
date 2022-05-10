using ReadList.Application.ViewModels;

namespace ReadList.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<StatisticsViewModel> Statistics(Guid userId);
    }
}
