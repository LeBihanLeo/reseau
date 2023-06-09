﻿using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;



namespace Owin
{

    public class TimeController : ApiController
    {
        RequestTool requestTool = new RequestTool();
        private int nbRequest = 5;

        public string Get()
        {
            List<string> urlList = new List<string>();
            Dictionary<string, TimeSpan> timeDict = new Dictionary<string, TimeSpan>();

            urlList.Add("http://sco.polytech.unice.fr/");
            urlList.Add("http://www.tigli.fr/");
            urlList.Add("https://lms.univ-cotedazur.fr/");
            urlList.Add("https://www.etoile.fr/");
            urlList.Add("https://www.maathiildee.com/");
            urlList.Add("https://www.quizz.biz/quizz-1076400.html");
            urlList.Add("https://www.complot.net/");
            urlList.Add("https://www.neige.fr/");

            
            timeDict = ResponseTimeStats(urlList);
            printDict(timeDict);
            var json = JsonConvert.SerializeObject(timeDict);
            return json;
        }

        public Dictionary<string, TimeSpan> ResponseTimeStats(List<string> urlList)
        {
            Dictionary<string, TimeSpan> timeStat = new Dictionary<string, TimeSpan>();

            foreach (string url in urlList)
            {
                List<TimeSpan> responseTime = new List<TimeSpan>();
                for (int i = 0; i < nbRequest; i++)
                {
                    TimeSpan time = GetResponseTime(url);
                    Console.WriteLine(time);
                    if(time  != null) responseTime.Add(time);
                }
                Console.WriteLine(responseTime.Count);
                
                TimeSpan average = computeAverage(responseTime);
                Console.WriteLine("average = "+average);

                timeStat.Add(url, average);
            }
            Console.WriteLine("timeStats = "+ timeStat.ToString());
            return timeStat;
        }

        public TimeSpan GetResponseTime(string url)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                requestTool.HttpGetRequestHeader(url, "");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            stopwatch.Stop();

            return stopwatch.Elapsed;
        }

        public TimeSpan computeAverage(List<TimeSpan> timeSpanList)
        {
            double doubleAverageTicks = timeSpanList.Average(timeSpan => timeSpan.Ticks);
            long longAverageTicks = Convert.ToInt64(doubleAverageTicks);

            return new TimeSpan(longAverageTicks);
        }
        public void printDict(Dictionary<string, TimeSpan> dict)
        {
            foreach (KeyValuePair<string, TimeSpan> kvp in dict)
            {
                Console.WriteLine("Clé = {0}, Valeur = {1}", kvp.Key, kvp.Value);
            }
        }
    }
    
    

}
