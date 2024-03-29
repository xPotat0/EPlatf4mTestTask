﻿

namespace UssJuniorTest.Core.Models
{
    public class AdvanceDriveLog : Model
    {
        public long PersonId { get; set; } // Порядковый номер человека

        public long CarId { get; set; } // Порядковый номер машины
        
        public string Name {  get; set; } // Имя водителя

        public int Age { get; set; } // Возраст водителя

        public string Manufacturer { get; set; } // Производитель машины

        public string Model { get; set; } // Модель машины

        public TimeSpan DrivingTime { get; set; } // Время за рулём
        //public int DrivingTimeDays { get; set; } // Время за рулём в днях
        //public int DrivingTimeHours { get; set; } // Время за рулём в часах
        //public int DrivingTimeMinutes { get; set; } // Время за рулём в минутах


    }
}
