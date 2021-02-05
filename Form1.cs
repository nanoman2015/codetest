using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;
using System.IO;
using System.Net;
using System.Xml;
using System.IO.Compression;

namespace CodeTest
{
    public partial class Form1 : Form
    {
        //private static readonly HttpClient httpClient = new HttpClient();
        //public List<string> EmailAddresses = new List<string>();

       // HttpClient httpClient = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }

        static readonly HttpClient client = new HttpClient();
        string getfilePath = Properties.Resources.index;
        private void Form1_Load(object sender, EventArgs e)
        {
            //CookieContainer cookies = new CookieContainer();
            //HttpClientHandler handler = new HttpClientHandler();
            //handler.CookieContainer = cookies;

            //string sEmail = "";
            //string sPass = "";

            //try
            //{
            //    //string url = "https://github.com/login";
            //    string url = "https://api.github.com/users/".Trim() + sEmail;
            //    HttpClient client = new HttpClient(handler);



            //    Uri uri = new Uri(url);
            //    var postData = new List<KeyValuePair<string, string>>
            //        {
            //            new KeyValuePair<string, string>("username", sEmail),
            //            new KeyValuePair<string, string>("password ", sPass)
            //        };

            //    HttpContent content = new FormUrlEncodedContent(postData);
            //    var cookieJar = new CookieContainer();
            //    handler = new HttpClientHandler
            //    {
            //        CookieContainer = cookieJar,
            //        UseCookies = true,
            //        UseDefaultCredentials = false
            //    };

            //     client = new HttpClient(handler)
            //    {
            //        BaseAddress = uri
            //    };



            //    //HttpResponseMessage response = client.GetAsync(url).Result;
            //    HttpResponseMessage response = await client.PostAsync(url, content);
            //    response.EnsureSuccessStatusCode();
            //    string body = await response.Content.ReadAsStringAsync();


            //    IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
            //    //foreach (Cookie cookie in responseCookies)
            //    //    this.txtOutput.Text = cookie.Name + ": " + cookie.Value;



            //    this.txtOutput.Text = body.ToString();
            //    //return body;
            //}
            //catch (Exception ex)
            //{
            //    //return e.ToString();
            //}

            //HTMLAgilityPack(getfilePath);
        }
       
        public void HTMLAgilityPack(string filePath)
        {
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

            // There are various options, set as needed
            htmlDoc.OptionFixNestedTags = true;

            // filePath is a path to a file containing the html
            //htmlDoc.Load(filePath);
            htmlDoc.LoadHtml(filePath);
           
            // Use:  htmlDoc.LoadHtml(xmlString);  to load from a string (was htmlDoc.LoadXML(xmlString)

            // ParseErrors is an ArrayList containing any errors from the Load statement
            if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
            {
                // Handle any parse errors as required

            }
            else
            {

                if (htmlDoc.DocumentNode != null)
                {
                    HtmlAgilityPack.HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

                    if (bodyNode != null)
                    {
                        // Do something with bodyNode
                    }
                }
            }
        }




        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var userName = this.txtEmail.Text;
            var passwd = this.txtPass.Text;

            var url = "https://api.github.com/users/";
            //var json = JsonConvert.SerializeObject(this.txtOutput.Text);
            //var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
                //client.DefaultRequestHeaders.Accept.Add(
                //        new MediaTypeWithQualityHeaderValue("application/json"));

                //var urlExtend = "repos/symfony/symfony/contributors";
                var urlExtend = userName;
                //var result = await client.GetAsync(urlExtend);
                HttpResponseMessage response = await client.GetAsync(urlExtend);
                //MessageBox.Show(response.StatusCode.ToString());
                if (response.StatusCode.ToString() == "OK")
                {

                    //var values = new Dictionary<string, string>
                    //{
                    //    { "username", userName },
                    //    { "password", passwd }
                    //};

                    //var data = new FormUrlEncodedContent(values);
                    //var responsePost = await client.PostAsync(url, data);
                    //var contentHtmlAgilityPack = await client.GetStringAsync(urlLogs);
                    //HTMLAgilityPack(contentHtmlAgilityPack.ToString());

                    var resp = await response.Content.ReadAsStringAsync();


                    //var resultHead = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
                    //var response = await client.PostAsync(url, data);
                    //string resultPost = response.Content.ReadAsStringAsync().Result;

                    // List<Contributor> contributors = JsonConvert.DeserializeObject<List<Contributor>>(resp);


                    var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                            Convert.ToBase64String(authToken));

                    //var responseJSON = await client.PostAsync(url, data);
                    var content = response.Content.ReadAsStringAsync().Result;
                    this.txtOutput.Text = content.ToString();
                }
            }

            //if (Regex.IsMatch(this.txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            //{

            //}
            //else
            //{
            //    MessageBox.Show("Invalid format", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }



        //Event will click button
        private void txtEmail_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnLogin.PerformClick();
            }
        }
        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnLogin.PerformClick();
            }
        }
    }

    internal class Person
    {
        private string v1;
        private string v2;

        public Person(string v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}
