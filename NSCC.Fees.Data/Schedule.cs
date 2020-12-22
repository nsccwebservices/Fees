//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NSCC.Fees.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Schedule
    {
        public int ScheduleID { get; set; }
        public int ProgramID { get; set; }
        public int LocationID { get; set; }
        public string Title { get; set; }
        public string Campus { get; set; }
        public Nullable<System.DateTime> FirstPaymentDate { get; set; }
        public Nullable<System.DateTime> ProgramEndDate { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> AcademicYearEndDate { get; set; }
        public bool HasUPass { get; set; }
        public bool IsPublished { get; set; }
    
        public virtual Location Location { get; set; }
        public virtual Program Program { get; set; }
    }
}
