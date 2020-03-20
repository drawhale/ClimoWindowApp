using Climo.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Climo.Controller
{
    public class ClimoController : IClimoController
    {
        private string _API_URL { get; set; }

        public ClimoController(string _API_URL)
        {
            this._API_URL = _API_URL;
        }

        public List<ClimoItem> GetList()
        {
            List<ClimoItem> climos = new List<ClimoItem>();

            try
            {
                WebRequest request = WebRequest.Create(_API_URL + "/list.php");
                request.ContentType = "application/json";

                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream); ;
                string responseFromServer = reader.ReadToEnd();

                JArray result = JArray.Parse(responseFromServer);

                foreach (var item in result)
                {
                    climos.Add(new ClimoItem(int.Parse(item["id"].ToString()), int.Parse(item["index"].ToString()), item["text"].ToString(), DateTime.Parse(item["create_datetime"].ToString())));
                }

                reader.Close();
                dataStream.Close();
                response.Close();
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return climos;
        }

        public void AddItem(ClimoItem item)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_API_URL + "/add.php");

                string postData = string.Format("index={0}&text={1}&create_datetime={2}", item.Index, WebUtility.HtmlEncode(item.Text), item.CreateDateTime);
                byte[] data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RemoveItem(int ID)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_API_URL + "/remove.php");

                string postData = string.Format("id={0}", ID);
                byte[] data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
