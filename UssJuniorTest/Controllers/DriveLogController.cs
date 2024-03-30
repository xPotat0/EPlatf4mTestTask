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
    public ActionResult<AdvanceDriveLog> GetDriveLogsAggregation(int logsPerPage = 3, int page = 1)
    {
        //return Ok(Data(DateTime.MinValue, DateTime.MaxValue, logsPerPage, page));
        return Ok(_advanceLogService.GetAdvanceDriveLogs(DateTime.MinValue, DateTime.MaxValue, logsPerPage, page));
    }


    [HttpGet("GetByTime")]
    public ActionResult<AdvanceDriveLog> GetByTime(DateTime timeStart = default(DateTime), DateTime timeEnd = default(DateTime), int logsPerPage = 3, int page = 1)
    {
        //return Ok(Data(timeStart, timeEnd, logsPerPage, page));
        return Ok(_advanceLogService.GetAdvanceDriveLogs(timeEnd, timeStart, logsPerPage, page));
    }
}