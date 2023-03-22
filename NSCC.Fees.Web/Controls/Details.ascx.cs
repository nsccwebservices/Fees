using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Config = NSCC.Fees.Web.Properties.Settings;
using NSCC.Fees.Business;
using NSCC.Fees.Business.Classes;
using NSCC.Fees.Data;
using NSCC.Fees.Web.Classes;

using NSCC.ExceptionManagement.Business;

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
            }
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
                if (_programId > 0)
                {
                    _program = _repository.GetProgram(_programId);
                }
                else if (!String.IsNullOrEmpty(_acadProg) && !String.IsNullOrEmpty(_acadPlan))
                {
                    _program = _repository.GetProgram(Config.Default.AcademicYear, _acadProg, _acadPlan);
                }

                if (_program != null && _program.IsPublished)
                {
                    plcFound.Visible = true;
                    plcNotFound.Visible = false;

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

                    lblTitle.Text = _program.Name;
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
                            litCollegeServiceFeeDomestic.Text = String.Format(Business.Constants.COMMA_FORMAT, (int)Math.Ceiling((decimal)collegeService.AmountDomestic / 2));
                            litCollegeServiceFeeInternational.Text = String.Format(Business.Constants.COMMA_FORMAT, (int)Math.Ceiling((decimal)collegeService.AmountInternational / 2));
                        }
                        else
                        {
                            litCollegeServiceFeeDomestic.Text = String.Format(Business.Constants.COMMA_FORMAT, collegeService.AmountDomestic);
                            litCollegeServiceFeeInternational.Text = String.Format(Business.Constants.COMMA_FORMAT, collegeService.AmountInternational);
                        }

                    }

                    CollegeFee healthAndDentalDomestic = _program.CollegeFees.FirstOrDefault(x => x.LookupName == Business.Constants.HEALTH_DENTAL);
                    CollegeFee healthAndDentalInternational = _program.CollegeFees.FirstOrDefault(x => x.LookupName == Business.Constants.HEALTH_DENTAL_INTERNATIONAL);

                    if (healthAndDentalDomestic != null)
                    {
                        litHealthAndDentalDomestic.Text = String.Format(Business.Constants.COMMA_FORMAT, healthAndDentalDomestic.AmountDomestic);
                    }

                    if (healthAndDentalInternational != null)
                    {
                        litHealthAndDentalInternational.Text = String.Format(Business.Constants.COMMA_FORMAT, healthAndDentalInternational.AmountInternational);
                    }

                    // if just international dental fee is checked in admin then we'll show "--" for the domesticfee otherwise it's blank
                    if (healthAndDentalDomestic != null && healthAndDentalInternational == null)
                    {
                        litHealthAndDentalInternational.Text = "--";
                    }

                    // if just domestic dental fee is checked in admin then we'll show "--" for the international fee otherwise it's blank if other international college fees are checked in admin
                    if (healthAndDentalInternational != null && healthAndDentalDomestic == null)
                    {
                        litHealthAndDentalDomestic.Text = "--";
                    }


                    plcHealthAndDental.Visible = (healthAndDentalDomestic != null || healthAndDentalInternational != null);


                    CollegeFee studentAssociation = _program.CollegeFees.FirstOrDefault(x => x.LookupName == Business.Constants.STUDENT_ASSOCIATION);
                    if (studentAssociation != null)
                    {
                        plcStudentAssociationFee.Visible = true;
                        if (_program.IsPartTime ?? false)
                        {
                            litStudentAssociationFeeDomestic.Text = String.Format(Business.Constants.COMMA_FORMAT, (int)Math.Ceiling((decimal)studentAssociation.AmountDomestic / 2));
                            litStudentAssociationFeeInternational.Text = String.Format(Business.Constants.COMMA_FORMAT, (int)Math.Ceiling((decimal)studentAssociation.AmountInternational / 2 ));
                        }
                        else
                        {
                            litStudentAssociationFeeDomestic.Text = String.Format(Business.Constants.COMMA_FORMAT, studentAssociation.AmountDomestic);
                            litStudentAssociationFeeInternational.Text = String.Format(Business.Constants.COMMA_FORMAT, studentAssociation.AmountInternational);
                        }

                    }

                    var amountUpassDomestic = 0;
                    var amountUpassInternational = 0;

                    if (_program.Schedules.Where(x => x.IsPublished).Any(x => x.HasUPass))
                    {
                        CollegeFee upass = _repository.GetCollegeFee(Config.Default.AcademicYear, Business.Constants.UPASS);
                        if (upass != null)
                        {
                            plcUPass.Visible = true;
                            amountUpassDomestic = upass.AmountDomestic;
                            amountUpassInternational = upass.AmountInternational;
                            litUPassDomestic.Text = String.Format(Business.Constants.COMMA_FORMAT, amountUpassDomestic);
                            litUPassInternational.Text = String.Format(Business.Constants.COMMA_FORMAT, amountUpassInternational);
                        }
                    }

                    CollegeFee parkingPass = _program.CollegeFees.FirstOrDefault(x => x.LookupName == Business.Constants.PARKING_PASS);
                    if (parkingPass != null)
                    {
                        plcParkingPass.Visible = true;
                        litParkingPassDomestic.Text = String.Format(Business.Constants.COMMA_FORMAT, parkingPass.AmountDomestic);
                        litParkingPassInternational.Text = String.Format(Business.Constants.COMMA_FORMAT, parkingPass.AmountInternational);
                    }

                    CollegeFee isf = _program.CollegeFees.FirstOrDefault(x => x.LookupName == Business.Constants.INTERNATIONAL_STUDENT_FEE);
                    if (isf != null && (_program.IsInternationalOffering ?? false))
                    {
                        plcInternationalStudentFee.Visible = true;

                        if (_program.IsPartTime ?? false)
                        {
                            litInternationalStudentFeeInternational.Text = String.Format(Business.Constants.COMMA_FORMAT, (int)Math.Ceiling((decimal)isf.AmountInternational / 2));
                        }
                        else
                        {
                            litInternationalStudentFeeInternational.Text = String.Format(Business.Constants.COMMA_FORMAT, isf.AmountInternational);
                        }
                    }

                    CollegeFee vhcf = _program.CollegeFees.FirstOrDefault(x => x.LookupName == Business.Constants.VIRTUAL_HEALTHCARE_FEE);
                    if (vhcf != null)
                    {
                        plcVirtualHealthcare.Visible = true;
                        litVirtualHealthcare.Text = String.Format(Business.Constants.COMMA_FORMAT, vhcf.AmountDomestic);
                        litVirtualHealthcareInternational.Text = String.Format(Business.Constants.COMMA_FORMAT, vhcf.AmountInternational);
                    }


                    #endregion

                    #region "Total of Tuition + College Fees"

                    var collegeFeesDomestic = CalculateCollegeFeesDomestic();
                    var collegeFeesInternational = CalculateCollegeFeesInternational();

                    var totalDomestic = (_program.Tuition.LookupName == Business.Constants.NON_STANDARD_TUITION ? _program.NonStandardTuitionDomestic ?? 0 : _program.Tuition.AmountDomestic) + amountUpassDomestic + collegeFeesDomestic;
                    var totalInternational = (_program.Tuition.LookupName == Business.Constants.NON_STANDARD_TUITION ? _program.NonStandardTuitionInternational ?? 0 : _program.Tuition.AmountInternational) + amountUpassInternational + collegeFeesInternational;

                    litTotalDomestic.Text = totalDomestic.ToString(Business.Constants.CURRENCY_FORMAT);
                    litTotalInternational.Text = totalInternational.ToString(Business.Constants.CURRENCY_FORMAT);


                    //Payment First Term Domestic = First Term Tuition Domestic + College Fees Domestic + Co - op fee Domestic (if mandatory) + U-Pass amount (if any Schedule has UPass checked)
                    litPaymentFirstTermDomestic.Text = ((_program.FirstTermTuitionDomestic ?? 0) + collegeFeesDomestic + amountUpassDomestic + (_program.CoopTypeID == Business.Constants.COOP_MANDATORY ? _program.AmountCoopDomestic ?? 0 : 0)).ToString(Business.Constants.CURRENCY_FORMAT);

                    //PaymentFirstTermInternational = First Term Tuition International + College Fees Domestic + Co - op fee International (if mandatory) + U-Pass amount (if any Schedule has UPass checked)
                    litPaymentFirstTermInternational.Text = ((_program.FirstTermTuitionInternational ?? 0) + collegeFeesInternational + amountUpassInternational + (_program.CoopTypeID == Business.Constants.COOP_MANDATORY ? _program.AmountCoopInternational ?? 0 : 0)).ToString(Business.Constants.CURRENCY_FORMAT);

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
                        litCoop.Text = _program.CoopTypeID == Business.Constants.COOP_MANDATORY ? Config.Default.LabelCoopMandatory : Config.Default.LabelCoopOptional;
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

                    rptCostItems.DataSource = _program.CostItems.OrderBy(p => p.Name);
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
                    litTotalCostDomestic.Text = (totalDomestic + (_program.CoopTypeID == Business.Constants.COOP_MANDATORY ? _program.AmountCoopDomestic ?? 0 : 0) + (_program.AmountTextBooks ?? 0) + (_program.AmountSupplies ?? 0) + _program.CostItems.Sum(item => item.Cost)).ToString(Business.Constants.CURRENCY_FORMAT);
                    litTotalCostInternational.Text = (totalInternational + (_program.CoopTypeID == Business.Constants.COOP_MANDATORY ? _program.AmountCoopInternational ?? 0 : 0) + (_program.AmountTextBooks ?? 0) + (_program.AmountSupplies ?? 0) + _program.CostItems.Sum(item => item.Cost)).ToString(Business.Constants.CURRENCY_FORMAT);

                    if (!String.IsNullOrEmpty(_program.NotesPayment))
                    {
                        litNotesPayment.Text = _program.NotesPayment.Trim();
                        plcNotesPayment.Visible = true;
                    }
                    #endregion

                    #region "Schedules - Repeater and Notes"

                    var schedules = _program.Schedules.Where(p => p.IsPublished).OrderBy(p => p.Location.ShortName).ThenBy(p => p.StartDate).ThenBy(p => p.AcademicYearEndDate).ToList();
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
                        plcColIntVirtualHealthcare.Visible = true;
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

                else
                {
                    plcFound.Visible = false;
                    plcNotFound.Visible = true;
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
                        case Business.Constants.INTERNATIONAL_STUDENT_FEE:

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
    }
}