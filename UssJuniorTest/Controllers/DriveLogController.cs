using Microsoft.AspNetCore.Mvc;
using UssJuniorTest.Core;
using UssJuniorTest.Core.Models;

namespace UssJuniorTest.Controllers;

[Route("api/driveLog")]
public class DriveLogController : Controller
{
    public DriveLogController()
    {

    }

    // TODO
    //DriveLog logs = new DriveLog();

    public List<DriveLog> logs = new List<DriveLog> {
        new DriveLog { Id = 1, CarId = 1, PersonId = 1000000},
        new DriveLog { Id = 2, CarId = 51, PersonId = 1000001},
        new DriveLog { Id = 1, CarId = 1, PersonId = 1000002},
    };

    [HttpGet("GetAll")]
    public ActionResult<DriveLog> GetDriveLogsAggregation()
    {

        return Ok(logs);
    }

    [HttpGet("GetById/{id}")]
    public ActionResult<DriveLog> GetDriveLogFirst(int id = 0)
    {
        return Ok(logs.FirstOrDefault(x => x.Id == id, new DriveLog { Id = -1, CarId = -1}));
    }
}