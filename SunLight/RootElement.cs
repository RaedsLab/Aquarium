using System;

namespace SunLight
{
    public class RootElement
    {
        public Results results { get; set; }
        public String status { get; set; }
    }
    
    public class Results
    {
        public String sunrise { get; set; }
        public String sunset { get; set; }
        public String solar_noon { get; set; }
        public int day_length { get; set; }
        public String civil_twilight_begin { get; set; }
        public String civil_twilight_end { get; set; }
        public String nautical_twilight_begin { get; set; }
        public String nautical_twilight_end { get; set; }
        public String astronomical_twilight_begin { get; set; }
        public String astronomical_twilight_end { get; set; }

    }
}
