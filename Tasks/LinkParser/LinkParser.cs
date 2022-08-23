using System.Net;
using System.Text.RegularExpressions;

namespace Tasks.LinkParser
{
    public class LinkParser
    {
        /// <summary>
        /// Получить все ссылки с сайта
        /// </summary>
        /// <param name="url">Ссылка на сайт</param>
        public static List<string> GetLinks(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream);

                var data = reader.ReadToEnd();

                response.Close();
                reader.Close();

                return GetListOfLinksFromHTMLData(data);
            }
            else
            {
                throw new Exception("Сайт недоступен");
            }
        }

        private static List<string> GetListOfLinksFromHTMLData(string html)
        {
            List<string> links = new List<string>();

            var pattern = @"https?:\/\/?(?:[\w.-]+\.[a-z]{2,5})(?:\/[\w.#\?@%={}-]+)*";

            var match = Regex.Matches(html, pattern);

            foreach (Match item in match)
            {
                links.Add(item.Value);
            }

            return links;
        }
    }
}
