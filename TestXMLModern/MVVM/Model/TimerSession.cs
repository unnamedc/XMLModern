using System;

namespace TestXMLModern.MVVM.Model
{
    public class TimerSession
    {
        // Id больше не нужен без базы данных
        // public int Id { get; set; } 

        public DateTime Date { get; set; }
        public int DurationMinutes { get; set; }
    }
}
