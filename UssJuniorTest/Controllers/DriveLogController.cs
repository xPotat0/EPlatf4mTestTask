using Microsoft.AspNetCore.Mvc;
using UssJuniorTest.Core;
using UssJuniorTest.Core.Models;
using UssJuniorTest.Infrastructure.Store;

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

    [HttpGet("GetByTime")]
    public ActionResult<DriveLog> GetByTime(DateTime timeStart, DateTime timeEnd)
    {
        var dataBase = new InMemoryStore();
        var logs = dataBase.GetAllDriveLogs();
        var cars = dataBase.GetAllCars();
        var names = dataBase.GetAllPersons();
        var result = new List<AdvanceDriveLog>();
        foreach(var log in logs)
        {
            if (timeStart <= log.StartDateTime && timeEnd >= log.EndDateTime)
            {
                double time = (log.StartDateTime.Day - log.EndDateTime.Day);
                result.Add(new AdvanceDriveLog
                {
                    Name = names.Where(x => x.Id == log.PersonId).Select(x => x.Name).First(),
                    Age = names.Where(x => x.Id == log.PersonId).Select(x => x.Age).First(),
                    Manufacturer = cars.Where(x => x.Id == log.CarId).Select(x => x.Manufacturer).First(),
                    Model = cars.Where(x => x.Id == log.CarId).Select(x => x.Model).First(),
                    DrivingTime = log.EndDateTime.Subtract(log.StartDateTime),
                }
                ) ;
            }
        }
        return Ok(result);
    }
}