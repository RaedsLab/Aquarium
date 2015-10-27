using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using WComp.Beans;

namespace WComp.Beans
{
	[Bean(Category="Aquarium")]
	public class MoonLight
	{	
		private int moonBrightness;

		public int MoonBrightness {
			get { 
				FireEvent(moonBrightness);
				return moonBrightness; 
			}
		}
		
		public delegate void IntValueEventHandler(int val);
		public event IntValueEventHandler GetMoonBrightnessValueEvent;
		
		private void FireEvent(int i) {
			if (GetMoonBrightnessValueEvent != null)
				moonBrightness = GetValue();
				GetMoonBrightnessValueEvent(i);
		}
		
		private int GetValue()
        {
            int brightness = -1;
            
            DateTime date = DateTime.Now;
            
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            int hour = date.Hour;
            int minute = date.Minute;
            
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
 
            string address = string.Format(
            "http://localhost/moon.php?y={0}&m={1}&d={2}&h={3}&min={4}&sec={5}",
            Uri.EscapeDataString("" + year),
            Uri.EscapeDataString("" + month),
            Uri.EscapeDataString("" + day),
            Uri.EscapeDataString("" + hour),
            Uri.EscapeDataString("" + minute),
            Uri.EscapeDataString("01"));
 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
            request.Method = "GET";
            request.Accept = "application/json";
            
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        string objJson = reader.ReadToEnd();
                        var dict = json_serializer.Deserialize<Dictionary<string, string>>(objJson);
                        brightness = int.Parse(dict["moon"]);
                        return brightness;
                    }
                }
            }
            catch (WebException)
            {
                return brightness;
            }
        }
		
	}
}