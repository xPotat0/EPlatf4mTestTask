using Microsoft.AspNetCore.Mvc;
using UssJuniorTest.Core;
using UssJuniorTest.Core.Models;
using UssJuniorTest.Infrastructure.Services;
using UssJuniorTest.Infrastructure.Store;

namespace UssJuniorTest.Controllers;

[Route("api/driveLog")]
public class DriveLogController : Controller
{
    private readonly IAdvanceLogService _advanceLogService;
    public DriveLogController(IAdvanceLogService advanceLogService)
    {
        _advanceLogService = advanceLogService;
    }

    // TODO
    //Сделать Json парсер для TimeSpan в формате D.HH:MM


    [HttpGet("GetAll")]
    public ActionResult<AdvanceDriveLog> GetDriveLogsAggregation(int logsPerPage = 3, int page = 1,
                                                                 string filterPersonName = "", string filterCarName = "",
                                                                 bool sortByPerson = false, bool sortByCar = false)
    {
        return Ok(_advanceLogService.GetAdvanceDriveLogs(DateTime.MinValue, DateTime.MaxValue, logsPerPage, page, filterPersonName, filterCarName, sortByPerson, sortByCar));
    }


    [HttpGet("GetByTime")]
    public ActionResult<AdvanceDriveLog> GetByTime(DateTime timeStart = default(DateTime), DateTime timeEnd = default(DateTime),
                                                    int logsPerPage = 3, int page = 1,
                                                    string filterPersonName = "", string filterCarName = "",
                                                    bool sortByPerson = false, bool sortByCar = false)
    {
        return Ok(_advanceLogService.GetAdvanceDriveLogs(timeStart, timeEnd, logsPerPage, page, filterPersonName, filterCarName, sortByPerson, sortByCar));
    }
}