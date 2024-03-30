using Microsoft.AspNetCore.Mvc.RazorPages;
using UssJuniorTest.Core.Models;

namespace UssJuniorTest.Infrastructure.Services
{
    public interface IAdvanceLogService
    {
        List<AdvanceDriveLog> GetAdvanceDriveLogs(DateTime startTime, DateTime endTime, int logsPerPage, int page, string filterPersonName, string filterCarName, bool sortByPerson, bool sortByCar);
    }
}
