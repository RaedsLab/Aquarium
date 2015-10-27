using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using WComp.Beans;

namespace WComp.Beans
{
    
    [Bean(Category="Aquarium")]
    public class SunLightJSON
    {
        private string json;
        private string lat;
        private string lon;
        
        public string Json {
            get { 
            	FireEvent(json);
            	return json; 
            }
        }
        
        public string Lat {
            set { 
            	lat = value;
            	FireLatEvent(lat);
                FireEvent(json);
            }
        }
        
        public string Lon {
            set { 
            	lon = value;
            	FireLonEvent(lon);
                FireEvent(json);
            }
        }

        public delegate void StringValueEventHandler(string val);
        
        public event StringValueEventHandler GetJsonEvent;
        public event StringValueEventHandler LatValueChanged;
        public event StringValueEventHandler LonValueChanged;
        
        private void FireEvent(string s) {
        	if (GetJsonEvent != null) {
        		if(lat != null && lon != null) {
        			/*
        			double a, b;
        			try {
        				a = double.Parse(lat);
        				b = double.Parse(lon);
        				if((a > 90 || a < -90) || (b > 180 || b < -180)) {
        					a = 36.7201600;
        					b = -4.4203400;
        				}
        			} catch (Exception) {
        				a = 36.7201600;
        				b = -4.4203400;
        			}
        			lat = a.ToString();
        			lon = b.ToString();
        			*/
        			json = GetValues(lat, lon);
        		}
        		else 
        		{
        			json = GetValues("0", "0"); //43620
        		}
        		GetJsonEvent(s);
        	}
        }
        
        private void FireLatEvent(string s) {
        	if (LatValueChanged != null)
                LatValueChanged(s);
        }
        
        private void FireLonEvent(string s) {
        	if (LonValueChanged != null)
                LonValueChanged(s);
        }
        
        private string GetValues(string lat, string lon) {
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
 
            string address = string.Format(
                "http://api.sunrise-sunset.org/json?lat={0}&lng={1}&date=today&formatted=0",
            Uri.EscapeDataString("" + lat),
            Uri.EscapeDataString("" + lon));
 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
            request.Method = "GET";
            request.Accept = "application/json";
            
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        // Read & Desirialize object into a <key,ValueEvent> dictionary
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                return ex.Message;
            }
        }
    }    
}