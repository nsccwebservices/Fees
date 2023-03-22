using System;
using System.Linq;
using System.Web.UI.WebControls;

using Config = NSCC.Fees.Web.Properties.Settings;
using NSCC.Fees.Business;
using NSCC.Fees.Business.Classes;
using NSCC.Fees.Data;

using NSCC.ExceptionManagement.Business;
using NLog;

namespace NSCC.Fees.Web.Controls
{
    public partial class AtoZ : System.Web.UI.UserControl
    {
        //private static Logger logger = LogManager.GetCurrentClassLogger();
        private FeesRepository _repository = null;


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this.Response.IsRequestBeingRedirected)
            {
                return;
            }

            _repository = new FeesRepository(new FeesEntities());

            try
            {
                var programFees = _repository.GetPrograms(Config.Default.AcademicYear, false); //don't include unpublished Program fee data;

                var lst = programFees.GroupBy(x => x.Name.Substring(0, 1).ToUpper(), (alphabet, programs) => new AlphaCount { Alphabet = alphabet, Programs = programs.OrderBy(x => x.Name).ToList() })
                    .OrderBy(x => x.Alphabet);

                this.rptLetters.DataSource = lst;
                this.rptLetters.DataBind();

                this.rptProgramList.DataSource = lst;
                this.rptProgramList.DataBind();

                var program = programFees.FirstOrDefault();
                if (program != null)
                {
                    litAcademicYear.Text = String.Format(litAcademicYear.Text, program.AcademicYear.Name);
                }
            }
            catch (System.Data.Entity.Core.EntityException ex) //database likely unreachable
            {
                NSCCExceptionLogger.LogException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, Severity.HIGH, "Fees");
            }
            catch (Exception ex)
            {
                NSCCExceptionLogger.LogException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex, Severity.LOW, "Fees");
            }
            finally
            {
                _repository.Dispose();
            }

        }

        protected void rptProgramList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                AlphaCount dataItem = (AlphaCount)e.Item.DataItem;
                Repeater rptPrograms = (Repeater)e.Item.FindControl("rptPrograms");
                if (rptPrograms != null)
                {
                    rptPrograms.DataSource = dataItem.Programs;
                    rptPrograms.DataBind();
                }
            }
        }

        //private void LogError(string method, Exception e)
        //{
        //    logger.Error("Method: {0} - {1} - {2}: ", String.Format("{0}.{1}()", this.GetType().FullName, method), e.Message, e.StackTrace);
        //}
    }
}