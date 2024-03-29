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
    //Сделать Json парсер для TimeSpan в формате D.HH:MM

    private List<AdvanceDriveLog> Data(DateTime timeStart, DateTime timeEnd, int logsPerPage, int page)
    {
        /*-----Получаем данные из InMemoryStore-----*/
        var dataBase = new InMemoryStore();
        var logs = dataBase.GetAllDriveLogs();
        var cars = dataBase.GetAllCars();
        var names = dataBase.GetAllPersons();
        var result = new List<AdvanceDriveLog>();
        /*-----Перебераем все данные-----*/
        foreach (var log in logs)
        {
            bool isFound = false;
            /*-----Собираем данные в модель-----*/
            var logCandidate = new AdvanceDriveLog
            {
                PersonId = log.PersonId,
                CarId = log.CarId,
                Name = names.Where(x => x.Id == log.PersonId).Select(x => x.Name).First(),
                Age = names.Where(x => x.Id == log.PersonId).Select(x => x.Age).First(),
                Manufacturer = cars.Where(x => x.Id == log.CarId).Select(x => x.Manufacturer).First(),
                Model = cars.Where(x => x.Id == log.CarId).Select(x => x.Model).First(),
                DrivingTime = log.EndDateTime.Subtract(log.StartDateTime),
            };

            /*-----Ищем в записанных данных схожие сущности-----*/

            foreach (var AdDrLog in result)
            {
                if (AdDrLog.PersonId == logCandidate.PersonId && AdDrLog.CarId == logCandidate.CarId)
                {
                    isFound = true;
                    AdDrLog.DrivingTime += logCandidate.DrivingTime;
                }
            }

            /*----- Проверяем, какая из функций была вызвана: взять всё или взять часть-----*/

            if (timeStart == default(DateTime) || timeEnd == default(DateTime))
            {
                if (!isFound) { result.Add(logCandidate); } //Если такой модели ещё не было, то добавляем
            }
            else if (timeStart <= log.StartDateTime && timeEnd >= log.EndDateTime)
            { if (!isFound) { result.Add(logCandidate); } } //Если такой модели ещё не было, то добавляем
        }
        return result.Skip((page - 1) * logsPerPage).Take(page * logsPerPage).ToList();
    }
    

    [HttpGet("GetAll")]
    public ActionResult<AdvanceDriveLog> GetDriveLogsAggregation(int logsPerPage = 3, int page = 1)
    {
        return Ok(Data(DateTime.MinValue, DateTime.MaxValue, logsPerPage, page));
    }


    [HttpGet("GetByTime")]
    public ActionResult<AdvanceDriveLog> GetByTime(DateTime timeStart = default(DateTime), DateTime timeEnd = default(DateTime), int logsPerPage = 3, int page = 1)
    {
        return Ok(Data(timeStart, timeEnd, logsPerPage, page));
    }
}