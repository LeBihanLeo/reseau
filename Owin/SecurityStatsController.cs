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


    public class SecurityController : ApiController
    {
        RequestTool requestTool = new RequestTool();

        public string Get()
        {
            List<string> urlList = new List<string>();
            int[] stats = new int[2];

            urlList.Add("http://sco.polytech.unice.fr/");
            urlList.Add("http://www.tigli.fr/");
            urlList.Add("https://lms.univ-cotedazur.fr/");
            urlList.Add("https://www.bienvenue-a-la-ferme.com/nouvelle-aquitaine/creuse/puy-malsignat/ferme/la-ferme-de-chez-cohade/144457");
            urlList.Add("https://www.ville-valbonne.fr");
            urlList.Add("https://www.etoile.fr/");
            urlList.Add("https://www.maathiildee.com/");
            urlList.Add("https://www.quizz.biz/quizz-1076400.html");
            urlList.Add("https://www.complot.net/");
            urlList.Add("https://www.neige.fr/");

            stats = StatsSecurity(urlList);
            var json = JsonConvert.SerializeObject(stats);
            Console.Write(json);
            return json;
        } 
        
        public int[] StatsSecurity(List<string> urlList)
        {
            int nbXContentTypeOption = 0;
            int nbStrictTransportSecurity = 0;


            foreach (string url in urlList)
            {
                Console.WriteLine(url);

                HttpResponseHeaders header  = requestTool.HttpGetRequestHeader(url, "");
                if (header == null)
                {
                    Console.WriteLine("header null");
                    continue;
                }
                Console.WriteLine(header);
                if (header.Contains("X-Content-Type-Options")) nbXContentTypeOption += 1;
                if (header.Contains("Strict-Transport-Security")) nbStrictTransportSecurity += 1;

            }
            return  new int[] { nbXContentTypeOption, nbStrictTransportSecurity, urlList.Count};
        }
    }
}
