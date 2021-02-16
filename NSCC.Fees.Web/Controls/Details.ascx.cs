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
                lblTitle.Text = String.Format(lblTitle.Text, _program.Name);
                lblProgramFees.Text = String.Format(lblProgramFees.Text, _program.AcademicYear.Name);

                //AcademicYear = entity.AcademicYear.Name,
                //    Name = entity.Name,
                //    TuitionType = entity.Tuition.Name,
                //    NonStandardTuitionDomestic = entity.NonStandardTuitionDomestic ?? 0,
                //    NonStandardTuitionInternational = entity.NonStandardTuitionInternational ?? 0,
                //    StudentAssociation = entity.CollegeFees.FirstOrDefault(x => x.LookupName == NSCC.Fees.Business.Constants.STUDENT_ASSOCIATION),
                //    HealthAndDental = entity.CollegeFees.FirstOrDefault(x => x.LookupName == NSCC.Fees.Business.Constants.HEALTH_DENTAL),
                //    CollegeService = entity.CollegeFees.FirstOrDefault(x => x.LookupName == NSCC.Fees.Business.Constants.COLLEGE_SERVICE),
                //    IsPartTime = entity.IsPartTime ?? false,
                //    IsInternational = entity.IsInternationalOffering ?? false,
                //    FirstTermTuitionDomestic = entity.FirstTermTuitionDomestic ?? 0,
                //    FirstTermTuitionInternational = entity.FirstTermTuitionInternational ?? 0,
                //    Textbooks = entity.AmountTextBooks ?? 0,
                //    ClassroomSupplies = entity.AmountTextBooks ?? 0,
                //    TotalAdditionalCostItems = entity.CostItems.Sum(item => item.Cost),

                litTuitionDomestic.Text = _program.Tuition.LookupName == Business.Constants.NON_STANDARD_TUITION ? (_program.NonStandardTuitionDomestic ?? 0).ToString(Business.Constants.CURRENCY_FORMAT) : _program.Tuition.AmountDomestic.ToString(Business.Constants.CURRENCY_FORMAT);
                litTuitionInternational.Text = _program.Tuition.LookupName == Business.Constants.NON_STANDARD_TUITION ? (_program.NonStandardTuitionInternational ?? 0).ToString(Business.Constants.CURRENCY_FORMAT) : _program.Tuition.AmountInternational.ToString(Business.Constants.CURRENCY_FORMAT);


                #region "College Fees"
                CollegeFee collegeService = _program.CollegeFees.FirstOrDefault(x => x.LookupName == Business.Constants.COLLEGE_SERVICE);
                if (collegeService != null)
                {
                    plcCollegeServiceFee.Visible = true;
                    litCollegeServiceFeeDomestic.Text = collegeService.AmountDomestic.ToString();
                    litCollegeServiceFeeInternational.Text = collegeService.AmountInternational.ToString();
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
                    litStudentAssociationFeeDomestic.Text = studentAssociation.AmountDomestic.ToString();
                    litStudentAssociationFeeInternational.Text = studentAssociation.AmountInternational.ToString();
                }

                if (_program.Schedules.Any(x => x.HasUPass))
                {
                    CollegeFee upass = _repository.GetCollegeFee(Config.Default.AcademicYear, Business.Constants.UPASS);
                    if (upass != null)
                    {
                        plcUPass.Visible = true;
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
                if (isf != null)
                {
                    litInternationalStudentFeeInternational.Text = isf.AmountInternational.ToString();
                }

                #endregion

                #region "Total of Tuition + College Fees"
                var totalDomestic = (_program.Tuition.LookupName == Business.Constants.NON_STANDARD_TUITION ? _program.NonStandardTuitionDomestic ?? 0 : _program.Tuition.AmountDomestic) + _program.CollegeFees.Sum(item => item.AmountDomestic);
                var totalInternational = (_program.Tuition.LookupName == Business.Constants.NON_STANDARD_TUITION ? _program.NonStandardTuitionInternational ?? 0 : _program.Tuition.AmountInternational) + _program.CollegeFees.Sum(item => item.AmountInternational);

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

                //if (!String.IsNullOrEmpty(_program.NotesCoop))
                //{
                //    litNotesCoop.Text = _program.NotesCoop.Trim();
                //    plcNotesCoop.Visible = true;
                //}
                #endregion

                #region "Textbooks"

                litTextbooksDomestic.Text = (_program.AmountTextBooks ?? 0).ToString(Business.Constants.CURRENCY_FORMAT);
                litTextbooksInternational.Text = litTextbooksDomestic.Text;

                if (!String.IsNullOrEmpty(_program.NotesTextBooks))
                {
                    litNotesTextbooks.Text = _program.NotesTextBooks.Trim();
                    plcNotesTextbooks.Visible = true;
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

                #region "Total Cost section - Tuition and college fees + co-op (if mandatory) + textbooks + additional program costs"
                litTotalCostDomestic.Text = (totalDomestic + (_program.CoopTypeID == Business.Constants.COOP_MANDATORY ? _program.AmountCoopDomestic ?? 0 : 0) + (_program.AmountTextBooks ?? 0) + _program.CostItems.Sum(item => item.Cost)).ToString(Business.Constants.CURRENCY_FORMAT);
                litTotalCostInternational.Text = (totalInternational + (_program.CoopTypeID == Business.Constants.COOP_MANDATORY ? _program.AmountCoopInternational ?? 0 : 0) + (_program.AmountTextBooks ?? 0) + _program.CostItems.Sum(item => item.Cost)).ToString(Business.Constants.CURRENCY_FORMAT);

                //if (!String.IsNullOrEmpty(_program.NotesPayment))
                //{
                //    litNotesPayment.Text = _program.NotesPayment.Trim();
                //    plcNotesPayment.Visible = true;
                //}
                #endregion

                #region "Schedules - Repeater and Notes"

                var schedules = _program.Schedules.Where(p => p.IsPublished).ToList();
                rptSchedules.DataSource = schedules;
                rptSchedules.DataBind();
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
                    plcColCanadianDollars.Visible = true;
                    plcColIntTuition.Visible = true;
                    plcColIntCollegeServiceFee.Visible = true;
                    plcColIntHealthandDental.Visible = true;
                    plcColIntStudentAssociation.Visible = true;
                    plcColIntUPass.Visible = true;
                    plcColIntParkingPass.Visible = true;
                    plcInternationalStudentFee.Visible = true;
                    plcColDomTotal.Visible = true;
                    plcColIntTotal.Visible = true;

                    plcColCoop.Visible = true;
                    plcColIntCoop.Visible = true;

                    plcColTextbooks.Visible = true;
                    plcColIntTextbooks.Visible = true;

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