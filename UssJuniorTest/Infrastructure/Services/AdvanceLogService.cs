using Microsoft.AspNetCore.Mvc.RazorPages;
using UssJuniorTest.Core.Models;
using UssJuniorTest.Infrastructure.Store;

namespace UssJuniorTest.Infrastructure.Services
{
    public class AdvanceLogService : IAdvanceLogService
    {
        private static InMemoryStore data = new InMemoryStore();

        private void checkAll(ref List<AdvanceDriveLog> list, ref AdvanceDriveLog log,
                              DateTime leftBorderTime, DateTime rightBorderTime,
                              DateTime startDateTime, DateTime endDateTime,
                              string filterPersonName, string filterCarName,
                              bool isFound)
        {
            if (leftBorderTime == default(DateTime)) leftBorderTime = DateTime.MinValue;
            if (rightBorderTime == default(DateTime)) rightBorderTime = DateTime.MaxValue;
            bool[] checks =
            {
                (leftBorderTime <= startDateTime && rightBorderTime >= endDateTime),
                log.Name.Contains(filterPersonName),
                log.Model.Contains(filterCarName),
                !isFound,
            };

            foreach(bool check in checks) 
            {
                if (!check) { return; }
            }
            list.Add(log);
        }

        public List<AdvanceDriveLog> GetAdvanceDriveLogs(DateTime leftBorderTime, DateTime rightBorderTime,
                                                        int logsPerPage, int page,
                                                        string filterPersonName, string filterCarName,
                                                        bool sortByPerson, bool sortByCar)
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

                checkAll(ref result, ref logCandidate, leftBorderTime, rightBorderTime, log.StartDateTime, log.EndDateTime , filterPersonName, filterCarName, isFound);

            }
            /*-----Сортируем, если требуется-----*/
            if (sortByCar)
                result = result.OrderBy(x => x.Model).ToList();

            if (sortByPerson)
                result = result.OrderBy(x => x.Name).ToList();

            return result
                .Skip((page - 1) * logsPerPage)
                .Take(logsPerPage)
                .ToList();
        }
    }
}
