using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Config = NSCC.Fees.Web.Properties.Settings;
using NSCC.Fees.Business;
using NSCC.Fees.Business.Classes;
using NSCC.Fees.Data;
using NSCC.Fees.Web.Classes;
using System.Web.UI.HtmlControls;
using System.Net;

namespace NSCC.Fees.Web.Controls
{
    public partial class Details : System.Web.UI.UserControl
    {
        private int _programId = 0;
        private string _acadProg = String.Empty;
        private string _acadPlan = String.Empty;

        private Data.Program _program = null;
        private FeesRepository _repository = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ParseQueryString();

                if (_programId > 0)
                {
                    LoadProgramFee();
                }
                else if (!String.IsNullOrEmpty(_acadProg) && !String.IsNullOrEmpty(_acadPlan))
                {
                    LoadProgramFeeByPlan();
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this.Response.IsRequestBeingRedirected)
            {
                return;
            }

            if (_program != null && _program.IsPublished)
            {

                //metadata - description
                if (!String.IsNullOrEmpty(Config.Default.MetaDescription))
                {
                    HtmlMeta meta = new HtmlMeta();
                    meta.Name = "description";
                    meta.Content = WebUtility.HtmlDecode(String.Format(Config.Default.MetaDescription, _program.Name));
                    this.Page.Header.Controls.Add(meta);
                }

                //metadata - title
                Page.Title = String.Concat(WebUtility.HtmlDecode(_program.Name), " | Fees | NSCC");

                lblTitle.Text = String.Format(lblTitle.Text, _program.Name);
                lblProgramFees.Text = String.Format(lblProgramFees.Text, _program.AcademicYear.Name);

                litTuitionDomestic.Text = _program.Tuition.LookupName == Business.Constants.NON_STANDARD_TUITION ? (_program.NonStandardTuitionDomestic ?? 0).ToString(Business.Constants.CURRENCY_FORMAT) : _program.Tuition.AmountDomestic.ToString(Business.Constants.CURRENCY_FORMAT);
                litTuitionInternational.Text = _program.Tuition.LookupName == Business.Constants.NON_STANDARD_TUITION ? (_program.NonStandardTuitionInternational ?? 0).ToString(Business.Constants.CURRENCY_FORMAT) : _program.Tuition.AmountInternational.ToString(Business.Constants.CURRENCY_FORMAT);


                #region "College Fees"
                CollegeFee collegeService = _program.CollegeFees.FirstOrDefault(x => x.LookupName == Business.Constants.COLLEGE_SERVICE);
                if (collegeService != null)
                {
                    plcCollegeServiceFee.Visible = true;
                    if (_program.IsPartTime ?? false)
                    {
                        litCollegeServiceFeeDomestic.Text = ((int)Math.Ceiling((decimal)collegeService.AmountDomestic / 2)).ToString();
                        litCollegeServiceFeeInternational.Text = ((int)Math.Ceiling((decimal)collegeService.AmountInternational / 2)).ToString();
                    }
                    else
                    {
                        litCollegeServiceFeeDomestic.Text = collegeService.AmountDomestic.ToString();
                        litCollegeServiceFeeInternational.Text = collegeService.AmountInternational.ToString();
                    }

                }

                CollegeFee healthAndDental = _program.CollegeFees.FirstOrDefault(x => x.LookupName == Business.Constants.HEALTH_DENTAL);
                if (healthAndDental != null)
                {
                    plcHealthAndDental.Visible = true;
                    litHealthAndDentalDomestic.Text = healthAndDental.AmountDomestic.ToString();
                    litHealthAndDentalInternational.Text = healthAndDental.AmountInternational.ToString();
                }

                CollegeFee studentAssociation = _program.CollegeFees.FirstOrDefault(x => x.LookupName == Business.Constants.STUDENT_ASSOCIATION);
                if (studentAssociation != null)
                {
                    plcStudentAssociationFee.Visible = true;
                    if (_program.IsPartTime ?? false)
                    {
                        litStudentAssociationFeeDomestic.Text = ((int)Math.Ceiling((decimal)studentAssociation.AmountDomestic / 2)).ToString();
                        litStudentAssociationFeeInternational.Text = ((int)Math.Ceiling((decimal)studentAssociation.AmountInternational / 2 )).ToString();
                    }
                    else
                    {
                        litStudentAssociationFeeDomestic.Text = studentAssociation.AmountDomestic.ToString();
                        litStudentAssociationFeeInternational.Text = studentAssociation.AmountInternational.ToString();
                    }

                }

                var amountUpass = 0;
                if (_program.Schedules.Any(x => x.HasUPass))
                {
                    CollegeFee upass = _repository.GetCollegeFee(Config.Default.AcademicYear, Business.Constants.UPASS);
                    if (upass != null)
                    {
                        plcUPass.Visible = true;
                        amountUpass = upass.AmountDomestic;
                        litUPassDomestic.Text = upass.AmountDomestic.ToString();
                        litUPassInternational.Text = upass.AmountInternational.ToString();
                    }
                }

                CollegeFee parkingPass = _program.CollegeFees.FirstOrDefault(x => x.LookupName == Business.Constants.PARKING_PASS);
                if (parkingPass != null)
                {
                    plcParkingPass.Visible = true;
                    litParkingPassDomestic.Text = parkingPass.AmountDomestic.ToString();
                    litParkingPassInternational.Text = parkingPass.AmountInternational.ToString();
                }

                CollegeFee isf = _program.CollegeFees.FirstOrDefault(x => x.LookupName == Business.Constants.INTERNATIONAL_STUDENT_FEE);
                if (isf != null && (_program.IsInternationalOffering ?? false))
                {
                    litInternationalStudentFeeInternational.Text = isf.AmountInternational.ToString();
                    plcInternationalStudentFee.Visible = true;
                }

                #endregion

                #region "Total of Tuition + College Fees"
                var totalDomestic = (_program.Tuition.LookupName == Business.Constants.NON_STANDARD_TUITION ? _program.NonStandardTuitionDomestic ?? 0 : _program.Tuition.AmountDomestic) + CalculateCollegeFeesDomestic();
                var totalInternational = (_program.Tuition.LookupName == Business.Constants.NON_STANDARD_TUITION ? _program.NonStandardTuitionInternational ?? 0 : _program.Tuition.AmountInternational) + CalculateCollegeFeesInternational();

                litTotalDomestic.Text = totalDomestic.ToString(Business.Constants.CURRENCY_FORMAT);
                litTotalInternational.Text = totalInternational.ToString(Business.Constants.CURRENCY_FORMAT);

                litPaymentFirstTermDomestic.Text = (_program.FirstTermTuitionDomestic ?? 0).ToString(Business.Constants.CURRENCY_FORMAT);
                litPaymentFirstTermInternational.Text = (_program.FirstTermTuitionInternational ?? 0).ToString(Business.Constants.CURRENCY_FORMAT);

                if (!String.IsNullOrEmpty(_program.NotesTuitionCollegeFees))
                {
                    litNotesTuition.Text = _program.NotesTuitionCollegeFees.Trim();
                    plcNotesTuition.Visible = true;
                }

                #endregion

                #region "Co-op"
                if (_program.CoopTypeID != Business.Constants.COOP_NONE)
                {
                    plcCoop.Visible = true;
                    litCoop.Text = String.Format(litCoop.Text, _program.CoopTypeID == Business.Constants.COOP_MANDATORY ? "mandatory" : "optional");
                    litCoopDomestic.Text = (_program.AmountCoopDomestic ?? 0).ToString(Business.Constants.CURRENCY_FORMAT);
                    litCoopInternational.Text = (_program.AmountCoopInternational ?? 0).ToString(Business.Constants.CURRENCY_FORMAT);
                }

                if (!String.IsNullOrEmpty(_program.NotesCoop))
                {
                    litNotesCoop.Text = _program.NotesCoop.Trim();
                    plcNotesCoop.Visible = true;
                }
                #endregion

                #region "Textbooks"

                if (_program.AmountTextBooks.HasValue)
                {
                    litTextbooksDomestic.Text = _program.AmountTextBooks.Value.ToString(Business.Constants.CURRENCY_FORMAT);
                    litTextbooksInternational.Text = litTextbooksDomestic.Text;
                    plcTextbooks.Visible = true;
                }

                if (!String.IsNullOrEmpty(_program.NotesTextBooks))
                {
                    litNotesTextbooks.Text = _program.NotesTextBooks.Trim();
                    plcNotesTextbooks.Visible = true;
                }

                #endregion

                #region "Classroom/Portfolio supplies"

                if (_program.AmountSupplies.HasValue)
                {
                    litSuppliesDomestic.Text = _program.AmountSupplies.Value.ToString(Business.Constants.CURRENCY_FORMAT);
                    litSuppliesInternational.Text = litSuppliesDomestic.Text;
                    plcSupplies.Visible = true;
                }

                #endregion

                #region "Cost Items - Repeater and Notes"

                rptCostItems.DataSource = _program.CostItems;
                rptCostItems.DataBind();
                plcCostItems.Visible = _program.CostItems.Count > 0;

                if (!String.IsNullOrEmpty(_program.NotesProgramCosts))
                {
                    litNotesCostItems.Text = _program.NotesProgramCosts.Trim();
                    plcNotesCostItems.Visible = true;
                }

                #endregion

                #region "Total Cost section"
                // Tuition and college fees + U-Pass amount (if any Schedule has UPass checked) + co-op (if mandatory) + textbooks + additional program costs + classroom/portfolio supplies"
                litTotalCostDomestic.Text = (totalDomestic + amountUpass + (_program.CoopTypeID == Business.Constants.COOP_MANDATORY ? _program.AmountCoopDomestic ?? 0 : 0) + (_program.AmountTextBooks ?? 0) + (_program.AmountSupplies ?? 0) + _program.CostItems.Sum(item => item.Cost)).ToString(Business.Constants.CURRENCY_FORMAT);
                litTotalCostInternational.Text = (totalInternational + amountUpass +  (_program.CoopTypeID == Business.Constants.COOP_MANDATORY ? _program.AmountCoopInternational ?? 0 : 0) + (_program.AmountTextBooks ?? 0) + (_program.AmountSupplies ?? 0) + _program.CostItems.Sum(item => item.Cost)).ToString(Business.Constants.CURRENCY_FORMAT);

                if (!String.IsNullOrEmpty(_program.NotesPayment))
                {
                    litNotesPayment.Text = _program.NotesPayment.Trim();
                    plcNotesPayment.Visible = true;
                }
                #endregion

                #region "Schedules - Repeater and Notes"

                var schedules = _program.Schedules.Where(p => p.IsPublished).ToList();
                rptSchedules.DataSource = schedules;
                rptSchedules.DataBind();

                lnkAcademicCalendar.HRef = Config.Default.AcademicCalendarURL;

                plcSchedules.Visible = schedules.Count > 0;


                if (!String.IsNullOrEmpty(_program.NotesSchedule))
                {
                    litNotesSchedule.Text = _program.NotesSchedule.Trim();
                    plcNotesSchedule.Visible = true;
                }

                #endregion

                if (!String.IsNullOrEmpty(_program.ProgramPageLink))
                {
                    lnkProgramPage.HRef = _program.ProgramPageLink;
                    plcProgramPageLink.Visible = true;
                }




                if (_program.IsInternationalOffering ?? false)//default to false if null
                {
 
                    plcColIntAmount.Visible = true;
                    plcColIntTuition.Visible = true;
                    plcColIntCollegeServiceFee.Visible = true;
                    plcColIntHealthandDental.Visible = true;
                    plcColIntStudentAssociation.Visible = true;
                    plcColIntUPass.Visible = true;
                    plcColIntParkingPass.Visible = true;
                    plcColDomTotal.Visible = true;
                    plcColIntTotal.Visible = true;

                    plcColCoop.Visible = true;
                    plcColIntCoop.Visible = true;

                    plcColTextbooks.Visible = true;
                    plcColIntTextbooks.Visible = true;

                    plcColSupplies.Visible = true;
                    plcColIntSupplies.Visible = true;

                    plcColTotalCost.Visible = true;
                    plcColIntTotalCost.Visible = true;
                }
                else
                {
                   plcInternationalText.Visible = true;
                }
            }
        }


        protected void rptCostItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                PlaceHolder plcColCostItems = (PlaceHolder)e.Item.FindControl("plcColCostItems");
                if (plcColCostItems != null)
                {
                    plcColCostItems.Visible = _program.IsInternationalOffering ?? false;
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PlaceHolder plcIntCostItems = (PlaceHolder)e.Item.FindControl("plcIntCostItems");
                if (plcIntCostItems != null)
                {
                    plcIntCostItems.Visible = _program.IsInternationalOffering ?? false;
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Literal litCostItemsTotalDomestic = (Literal)e.Item.FindControl("litCostItemsTotalDomestic");
                if (litCostItemsTotalDomestic != null)
                {
                    litCostItemsTotalDomestic.Text = _program.CostItems.Sum(item => item.Cost).ToString(Business.Constants.CURRENCY_FORMAT);
                }

                PlaceHolder plcIntColCostItemsTotal = (PlaceHolder)e.Item.FindControl("plcIntColCostItemsTotal");
                if (plcIntColCostItemsTotal != null)
                {
                    Literal litCostItemsTotalInternational = (Literal)plcIntColCostItemsTotal.FindControl("litCostItemsTotalInternational");
                    if (litCostItemsTotalInternational != null)
                    {
                        litCostItemsTotalInternational.Text = _program.CostItems.Sum(item => item.Cost).ToString(Business.Constants.CURRENCY_FORMAT);
                    }

                    plcIntColCostItemsTotal.Visible = _program.IsInternationalOffering ?? false;
                }
            }
        }


        private int CalculateCollegeFeesDomestic()
        {
            var sum = 0;
            if (_program.IsPartTime ?? false)
            {
                foreach (CollegeFee fee in _program.CollegeFees)
                {
                    switch (fee.LookupName)
                    {
                        //upass is added in separately with the schedules
                        case Business.Constants.UPASS:

                            break;

                        case Business.Constants.COLLEGE_SERVICE:
                        case Business.Constants.STUDENT_ASSOCIATION:

                            sum += (int)Math.Ceiling((decimal)fee.AmountDomestic / 2);

                            break;

                        default:

                            sum += fee.AmountDomestic;
                            break;
                    }
                }
            }
            else
            {
                sum = _program.CollegeFees.Sum(item => item.AmountDomestic);
            }

            return sum;
        }
        private int CalculateCollegeFeesInternational()
        {
            var sum = 0;
            if (_program.IsPartTime ?? false)
            {
                foreach (CollegeFee fee in _program.CollegeFees)
                {
                    switch (fee.LookupName)
                    {
                        //upass is added in separately with the schedules
                        case Business.Constants.UPASS:

                            break;

                        case Business.Constants.COLLEGE_SERVICE:
                        case Business.Constants.STUDENT_ASSOCIATION:

                            sum += (int)Math.Ceiling((decimal)fee.AmountInternational / 2);

                            break;

                        default:

                            sum += fee.AmountInternational;
                            break;
                    }
                }
            }
            else
            {
                sum = _program.CollegeFees.Sum(item => item.AmountInternational);
            }

            return sum;
        }

        private void ParseQueryString()
        {
            Int32.TryParse(Request.QueryString[Web.Classes.Constants.QS_KEY_PROGRAM_ID], out _programId);
            _acadProg = Request.QueryString[Web.Classes.Constants.QS_KEY_ACADEMIC_PROGRAM];
            _acadPlan = Request.QueryString[Web.Classes.Constants.QS_KEY_ACADEMIC_PLAN];
        }

        private void LoadProgramFee()
        {
            _repository = new FeesRepository(new FeesEntities());
            _program = _repository.GetProgram(_programId);
        }

        private void LoadProgramFeeByPlan()
        {
            _repository = new FeesRepository(new FeesEntities());
            _program = _repository.GetProgram(Config.Default.AcademicYear, _acadProg, _acadPlan);
        }
    }
}