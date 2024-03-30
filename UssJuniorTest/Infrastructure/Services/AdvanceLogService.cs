using Microsoft.AspNetCore.Mvc.RazorPages;
using UssJuniorTest.Core.Models;
using UssJuniorTest.Infrastructure.Store;

namespace UssJuniorTest.Infrastructure.Services
{
    public class AdvanceLogService : IAdvanceLogService
    {
        private static InMemoryStore data = new InMemoryStore();
        public List<AdvanceDriveLog> GetAdvanceDriveLogs(DateTime timeStart, DateTime timeEnd, int logsPerPage, int page)
        {
           /*-----Получаем данные из InMemoryStore-----*/
            var logs = data.GetAllDriveLogs();
            var cars = data.GetAllCars();
            var names = data.GetAllPersons();
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

                if (timeStart == default(DateTime) || timeEnd == default(DateTime))    // Взять всё
                {
                    if (!isFound) { result.Add(logCandidate); }                        //Если такой модели ещё не было, то добавляем
                }
                else if (timeStart <= log.StartDateTime && timeEnd >= log.EndDateTime) // Взять часть
                {
                    if (!isFound) { result.Add(logCandidate); }                        //Если такой модели ещё не было, то добавляем
                } 
            }
            
            return result.Skip((page - 1) * logsPerPage).Take(logsPerPage).ToList();
        }
    }
}
