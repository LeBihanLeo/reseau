﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;

namespace Owin
{
    internal class RequestTool
    {

        

        public HttpResponseHeaders HttpGetRequestHeader(string uri, string url)
        {
            HttpResponseHeaders header = null;
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                try
                {
                    client.BaseAddress = new Uri(uri);
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                   header = response.Headers;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return header;
            }
        }


        
    }
}
