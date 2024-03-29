﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NSCC.Fees.Data;

namespace NSCC.Fees.Business
{
    public interface IFeesRepository : IDisposable
    {

        IEnumerable<AcademicYear> GetAcademicYears();
        IEnumerable<AcademicYear> GetAcademicYears(int count);
        AcademicYear GetAcademicYear(int acadyear);

        IEnumerable<Program> GetPrograms(bool includeUnpublished = true);
        IEnumerable<Program> GetPrograms(int acadyear, bool includeUnpublished = true);
        Program GetProgram(int programID);
        Program GetProgram(int acadyear, string program, string plan);


        void UpdateProgram(Program program, List<int> collegeFees);
        void AddProgram(Program program);
        void DeleteProgram(int programID);

        IEnumerable<Schedule> GetSchedules();
        IEnumerable<Schedule> GetSchedules(int programID);
        IEnumerable<Schedule> GetSchedulesByAcademicYear(int acadyear);

        IEnumerable<CostItem> GetCostItemsByAcademicYear(int acadyear);
        Schedule GetSchedule(int scheduleID);
        void AddSchedule(Schedule sch);
        void DeleteSchedule(int scheduleID);
        void UpdateSchedule(Schedule sch);

        IEnumerable<CollegeFee> GetCollegeFees(int acadyear, bool includeUPass);
        CollegeFee GetCollegeFee(int collegeFeeID);
        CollegeFee GetCollegeFee(int acadyear, string lookupName);
        CollegeFee GetCollegeFee(string lookupName);
        void UpdateCollegeFee(CollegeFee entity);

        IEnumerable<CostItem> GetCostItems(int programID);
        CostItem GetCostItem(int costItemID);
        void AddCostItem(CostItem entity);
        void UpdateCostItem(CostItem entity);
        void DeleteCostItem(int costItemID);

        IEnumerable<Tuition> GetTuitions();
        IEnumerable<Tuition> GetTuitions(int acadyear);
        Tuition GetTuition(int tuitionID);
        Tuition GetTuition(int acadyear, string lookupName);
        Tuition GetTuition(string lookupName);
        void AddTuition(Tuition entity);
        void UpdateTuition(Tuition entity);
        void DeleteTuition(int tuitionID);

        IEnumerable<Location> GetLocations();
        IEnumerable<Delivery> GetDeliveries();
        IEnumerable<Location> GetMetroCampuses();
        IEnumerable<School> GetSchools();

        void Save();
    }
}
