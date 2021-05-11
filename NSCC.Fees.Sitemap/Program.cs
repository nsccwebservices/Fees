using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Config = NSCC.Fees.Sitemap.Properties.Settings;
using NSCC.Fees.Business;
using NSCC.Fees.Business.Classes;
using NSCC.Fees.Data;


using NLog;

namespace NSCC.Fees.Sitemap
{
    class Program
    {

        static string xmlnamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

        private static Properties.Settings _config = new Properties.Settings();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            var repository = new FeesRepository(new FeesEntities());
            var urls = new List<URL>();

            try
            {
                var programFees = repository.GetPrograms(Config.Default.AcademicYear, false); //don't include unpublished Program fee data;

                foreach (Data.Program p in programFees)
                {
                    string link = String.Format(_config.FeesDetailsLink, p.ProgramID);
                    urls.Add(new URL { loc = link, lastmod = DateTime.Now.ToString("yyyy-MM-dd") });
                }

                XmlSerializer ser = new XmlSerializer(typeof(List<URL>), null, null, new XmlRootAttribute("urlset"), xmlnamespace);
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(String.Empty, xmlnamespace);

                using (TextWriter txtWriter = new StreamWriter(_config.OutputPath, false, Encoding.UTF8))
                {
                    ser.Serialize(txtWriter, urls, ns);
                }
            }
            catch (Exception e)
            {
                logger.Error("{0} - {1}: ", e.Message, e.StackTrace);
            }
            finally
            {
                repository.Dispose();
            }
        }
    }

    [XmlType(TypeName = "url")]
    public class URL
    {
        public string loc { get; set; }
        public string lastmod { get; set; }
    }
}
