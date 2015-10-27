using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using WComp.Beans;

namespace SunLight
{
	[Bean(Category="Aquarium")]
	public class SunLight
	{
		private string json;
		private string sunrise;
        private string sunset;
        private string solarNoon;
        private int dayLength;
        
        public string Json {
        	get { return json; }
            set {
            	json = value;
            	FireJsonEvent(json);
            }
        }
		
		public string Sunrise {
        	get {
        		FireSunriseEvent(sunrise);
        		return sunrise;
        	}
        }
        
        public string Sunset {
        	get {
        		FireSunsetEvent(sunset);
        		return sunset;
        	}
        }
        
        public string SolarNoon {
        	get {
        		FireSolarNoonEvent(solarNoon);
        		return solarNoon;
        	}
        }
        
        public int DayLength {
        	get {
        		FireDayLengthEvent(dayLength);
        		return dayLength;
        	}
        }

		public delegate void StringValueEventHandler(string val);
		public delegate void IntValueEventHandler(int val);
		
		public event StringValueEventHandler JsonValueChanged;
		public event StringValueEventHandler GetSunriseValueEvent;
		public event StringValueEventHandler GetSunsetValueEvent;
		public event StringValueEventHandler GetSolarNoonValueEvent;
		public event IntValueEventHandler GetDayLenghtValueEvent;
		
		private void FireSunriseEvent(string s) {
			if (GetSunriseValueEvent != null) {
                GetSunriseValueEvent(s);
			}
		}
		
		private void FireSunsetEvent(string s) {
			if (GetSunsetValueEvent != null) {
                GetSunsetValueEvent(s);
			}
		}
		
		private void FireSolarNoonEvent(string s) {
			if (GetSolarNoonValueEvent != null) {
                GetSolarNoonValueEvent(s);
			}
		}
		
		private void FireDayLengthEvent(int i) {
			if (GetDayLenghtValueEvent != null) {
                GetDayLenghtValueEvent(i);
			}
		}
		
		private void FireJsonEvent(string s) {
			if (JsonValueChanged != null) {
        		GetValues();
                JsonValueChanged(s);
			}
        }
		
		public void GetValues() {
			JavaScriptSerializer json_serializer = new JavaScriptSerializer();
			RootElement root = json_serializer.Deserialize<RootElement>(json);
			sunrise = root.results.sunrise;
			sunset = root.results.sunset;
			solarNoon = root.results.solar_noon;
			dayLength = root.results.day_length;
		}
		
	}
}