﻿using System;
using WComp.Beans;

namespace WComp.Beans
{
	[Bean(Category="Aquarium")]
	public class SunOrMoonChooser
	{
		//on récupére les deux résultats (sun & moon)
		//on prend les valeurs de sunset, sunrise, solarNoon nbadlouhom DateTime
		//on compare selon DateTime.Now
		//si entre sunset et sunrise on la compare par rapport à sunNoon (nb seconds)
		//sinon on retourne la valeur de noon
		
		//MoonLight
		private int moonBrightness;
		
		public int MoonBrightness {
			set {
				moonBrightness = value;
				FireMoonBrightnessEvent(moonBrightness);
			}
		}
		//Moon Light
		
		//SunLight
		private string sunset;
		private string sunrise;
		private string solarNoon;
		
		public string Sunset {
			set {
				sunset = value;
				FireSunsetEvent(sunset);
			}
		}
		
		public string Sunrise {
			set {
				sunrise = value;
				FireSunriseEvent(sunrise);
			}
		}
		
		public string SolarNoon {
			set {
				solarNoon = value;
				FireSolarNoonEvent(solarNoon);
			}
		}
		//SunLight
		
		//Result
		private string result;
		
		public string Result {
			get {
				FireEvent(result);
				return result; 
			}
		}
		//Result

		private void Choose()
        {
            DateTime now = DateTime.Now;

            DateTime sunriseTime =
                DateTime.ParseExact(sunrise, "yyyy-MM-ddTHH:mm:ss+00:00", null);

            DateTime sunsetTime =
                DateTime.ParseExact(sunset, "yyyy-MM-ddTHH:mm:ss+00:00", null);

            if (now > sunriseTime && now < sunsetTime)
            {
                DateTime solarNoonTime =
                    DateTime.ParseExact(solarNoon, "yyyy-MM-ddTHH:mm:ss+00:00", null);

                double val = (new TimeSpan(now.Hour, now.Minute, now.Second).TotalSeconds * 100) / new TimeSpan(solarNoonTime.Hour, solarNoonTime.Minute, solarNoonTime.Second).TotalSeconds;
                if (now > solarNoonTime)
                {
                    val = 100 - (val - 100);
                }
                result = val.ToString();
            }
            else
            {
            	result = moonBrightness.ToString();
            }
        }

		public delegate void StringValueEventHandler(string val);
		public event StringValueEventHandler SunsetValueChanged;
		public event StringValueEventHandler SunriseValueChanged;
		public event StringValueEventHandler SolarNoonValueChanged;
		public event StringValueEventHandler GetResultEvent;
		
		public delegate void IntValueEventHandler(int val);
		public event IntValueEventHandler MoonBrightnessValueChanged;
		
		private void FireEvent(string s) {
			if(GetResultEvent != null) {
				Choose();
				GetResultEvent(s);
			}
		}
		
		private void FireSunsetEvent(string s) {
			if (SunsetValueChanged != null)
				SunsetValueChanged(s);
		}
		
		private void FireSunriseEvent(string s) {
			if (SunriseValueChanged != null)
				SunriseValueChanged(s);
		}
		
		private void FireSolarNoonEvent(string s) {
			if (SolarNoonValueChanged != null)
				SolarNoonValueChanged(s);
		}
		
		private void FireMoonBrightnessEvent(int i) {
			if (MoonBrightnessValueChanged != null)
				MoonBrightnessValueChanged(i);
		}
	}
}
