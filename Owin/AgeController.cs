﻿using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;



namespace Owin
{

    public class AgeController : ApiController
    {
        RequestTool requestTool = new RequestTool();

        public string Get()
        {
            List<string> urlList = new List<string>();
            List<TimeSpan> ages = new List<TimeSpan>();

            urlList.Add("Chat");
            urlList.Add("Chien");
            urlList.Add("Loup_gris_commun");
            urlList.Add("Caucase");
            urlList.Add("Karatcha%C3%AF%C3%A9vo-Tcherkessie");
            urlList.Add("Felidae");
            urlList.Add("Proailurus");
            urlList.Add("Mongolie");
            urlList.Add("Tugrik");
            urlList.Add("Cercle");
            urlList.Add("Masse_volumique");
            urlList.Add("Alphabet_grec");
            urlList.Add("Écriture_bicamérale");
            urlList.Add("Bas_de_casse");
            urlList.Add("Chiffres_elzéviriens");
            urlList.Add("Hoefler_Text");

            ages = AgesStats("https://fr.wikipedia.org/wiki/", urlList);
            TimeSpan  average = computeAverage(ages);
            TimeSpan  deviation = computeDeviation(ages, average);

            Console.WriteLine("Average = " + average);
            Console.WriteLine("Deviation = " + deviation);
            var stats = new
            {
                average = average,
                deviation = deviation
            };

            var json = JsonConvert.SerializeObject(stats);
            return json;
        }
        
        public List<TimeSpan> AgesStats(string uri, List<string> urlList)
        {
            List<TimeSpan> ages = new List<TimeSpan>();
            foreach (string url in urlList)
            {
                try
                {
                    HttpResponseHeaders header = requestTool.HttpGetRequestHeader(uri, url);
                    if (header == null) continue;
                    if (header.Age != null)
                    {
                        TimeSpan age = (TimeSpan)header.Age;
                        ages.Add(age);
                        Console.WriteLine(uri + url + " : " + age);
                    }
                    else
                        Console.WriteLine("no Age in header");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return ages;
        }
        
        public TimeSpan computeAverage(List<TimeSpan> timeSpanList)
        {
            double doubleAverageTicks = timeSpanList.Average(timeSpan => timeSpan.Ticks);
            long longAverageTicks = Convert.ToInt64(doubleAverageTicks);

            return new TimeSpan(longAverageTicks);
        }

        public TimeSpan computeDeviation(List<TimeSpan> timeSpanList, TimeSpan average)
        {
            // Calculer la somme des carrés des différences
            double sumOfSquares = timeSpanList.Sum(ts => Math.Pow(ts.Ticks - average.Ticks, 2));

            // Diviser la somme obtenue par le nombre total de TimeSpan dans la liste
            double variance = sumOfSquares / timeSpanList.Count;

            // Prendre la racine carrée du résultat obtenu dans l'étape 3 pour obtenir l'écart-type
            double standardDeviation = Math.Sqrt(variance);
            long longStandardDeviation = Convert.ToInt64(standardDeviation);
            return new TimeSpan(longStandardDeviation);
        }
    }
}
