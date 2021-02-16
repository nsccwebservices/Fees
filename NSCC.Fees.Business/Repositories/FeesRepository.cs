using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NSCC.Fees.Data;

namespace NSCC.Fees.Business
{
    public class FeesRepository : IFeesRepository, IDisposable
    {
        #region "Private members"

        private FeesEntities _context;
        private bool disposed = false;

        #endregion

        #region "Constructors"
        public FeesRepository(FeesEntities context)
        {
            this._context = context;
        }
        #endregion

        #region "Academic Years"

        public IEnumerable<AcademicYear> GetAcademicYears(int count)
        {
            var items = _context.AcademicYears;
            return items.Where(p => p.IsPublished == true)
                            .OrderBy(e => e.AcademicYearID)
                            .Take(count)
                            .ToList();
        }

        public IEnumerable<AcademicYear> GetAcademicYears()
        {
            var items = _context.AcademicYears;
            return items.ToList();
        }

        public AcademicYear GetAcademicYear(int academicYearId)
        {
            var items = _context.AcademicYears;
            return items.Where(i => i.AcademicYearID == academicYearId).FirstOrDefault();
        }

        #endregion

        #region "Programs"
        public IEnumerable<Program> GetPrograms(bool includeUnpublished = true)
        {
            try
            {
                var programs = _context.Programs;//.Include(e => e.Categories).Include(e => e.Locations);
                if (!includeUnpublished)
                {
                    return programs.Where(i => i.IsPublished == true).ToList();
                }
                return programs.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Program> GetPrograms(int acadyear, bool includeUnpublished = true)
        {
            try
            {
                var programs = _context.Programs;
                if (!includeUnpublished)
                {
                    return programs.Where(i => i.AcademicYearID == acadyear && i.IsPublished == true).ToList();
                }
                return programs.Where(i => i.AcademicYearID == acadyear).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Program GetProgram(int programID)
        {
            try
            {
                var programs = _context.Programs;
                return programs.Where(p => p.ProgramID == programID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Program GetProgram(int acadyear, string program, string plan)
        {
            try
            {
                var programs = _context.Programs;
                return programs.Where(p => p.AcademicYearID == acadyear && String.Compare(p.AcadProg, program, true) == 0 && String.Compare(p.AcadPlan, plan, true) == 0).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateProgram(Program program, List<int> collegeFees)
        {
            try
            {
                if (collegeFees.Count() > 0)
                {
                    foreach (CollegeFee fee in program.CollegeFees.ToList())
                    {
                        program.CollegeFees.Remove(fee);
                    }

                    foreach (int i in collegeFees)
                    {
                        var cf = _context.CollegeFees.FirstOrDefault(s => s.CollegeFeeID == i);
                        if (cf != null)
                        {
                            program.CollegeFees.Add(cf);
                        }
                    }
                }
           
                _context.Entry(program).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AddProgram(Program program)
        {
            try
            {

                foreach (CollegeFee cf in program.CollegeFees.OrEmptyIfNull())
                {
                    _context.CollegeFees.Attach(cf);
                }

                _context.Programs.Add(program);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteProgram(int programID)
        {
            var entity = _context.Programs.Find(programID);
            if (entity != null)
            {
                var schedules = GetSchedules(programID);
                foreach (Schedule sch in schedules.OrEmptyIfNull())
                {
                    DeleteSchedule(sch.ScheduleID);
                }

                var costItems = GetCostItems(programID);

                foreach (CostItem ct in costItems.OrEmptyIfNull())
                {
                    DeleteCostItem(ct.CostItemID);
                }

                foreach (CollegeFee cf in entity.CollegeFees.ToList())
                {
                    entity.CollegeFees.Remove(cf);
                }

                _context.Programs.Remove(entity);
            }
        }
        #endregion

        #region "Schedules"

        public IEnumerable<Schedule> GetSchedules()
        {
            try
            {
                var schedules = _context.Schedules;
                return schedules.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Schedule> GetSchedules(int programID)
        {
            try
            {
                var program = GetProgram(programID);
                return program.Schedules.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //GetSchedulesByAcademicYear(int acadyear);
        public IEnumerable<Schedule> GetSchedulesByAcademicYear(int acadyear)
        {
            try
            {
                var schedules = _context.GetSchedulesByAcademicYear(acadyear);
                return schedules;
                // var program = GetProgram(programID);
                //  return program.Schedules.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Schedule GetSchedule(int scheduleID)
        {
            try
            {
                var schedules = GetSchedules();
                return schedules.Where(e => e.ScheduleID == scheduleID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddSchedule(Schedule sch)
        {
            try
            {
                //foreach (Category category in evt.Categories.OrEmptyIfNull())
                //{
                //    _context.Categories.Attach(category);
                //}

                //foreach (Location location in evt.Locations.OrEmptyIfNull())
                //{
                //    _context.Locations.Attach(location);
                //}

                _context.Schedules.Add(sch);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void DeleteSchedule(int scheduleID)
        {
            Schedule sch = _context.Schedules.Find(scheduleID);
            if (sch != null)
            {
                //foreach (Category cat in evt.Categories.ToList())
                //{
                //    evt.Categories.Remove(cat);
                //}

                //foreach (Location loc in evt.Locations.ToList())
                //{
                //    evt.Locations.Remove(loc);
                //}
                _context.Schedules.Remove(sch);
            }
        }

        public void UpdateSchedule(Schedule sch)
        {
            try
            {
                //foreach (Category cat in evt.Categories.ToList())
                //{
                //    evt.Categories.Remove(cat);
                //}

                //foreach (Location loc in evt.Locations.ToList())
                //{
                //    evt.Locations.Remove(loc);
                //}

                //foreach (int i in categories.OrEmptyIfNull())
                //{
                //    var category = _context.Categories.FirstOrDefault(s => s.CategoryID == i);
                //    if (category != null)
                //    {
                //        evt.Categories.Add(category);
                //    }
                //}

                //foreach (int i in locations.OrEmptyIfNull())
                //{
                //    var location = _context.Locations.FirstOrDefault(s => s.LocationID == i);
                //    if (location != null)
                //    {
                //        evt.Locations.Add(location);
                //    }
                //}

                _context.Entry(sch).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region "Locations"
        public IEnumerable<Location> GetLocations()
        {
            try
            {
                var locations = _context.Locations;
                return locations.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Location> GetMetroCampuses()
        {
            try
            {
                var locations = _context.Locations;
                return locations.Where(loc => loc.IsMetroCampus).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region "Cost Items"
        public IEnumerable<CostItem> GetCostItems(int programID)
        {
            try
            {
                var program = GetProgram(programID);
                return program.CostItems.ToList();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public CostItem GetCostItem(int costItemID)
        {
            try
            {
                var costItems = _context.CostItems;
                return costItems.Where(p => p.CostItemID == costItemID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AddCostItem(CostItem entity)
        {
            try
            {
                _context.CostItems.Add(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateCostItem(CostItem entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteCostItem(int costItemId)
        {
            CostItem ci = _context.CostItems.Find(costItemId);
            if (ci != null)
            {
                try
                {
                    _context.CostItems.Remove(ci);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        #endregion


        #region "Tuition"
        public IEnumerable<Tuition> GetTuitions(int acadyear)
        {
            try
            {
                var entities = _context.Tuitions;
                return entities.Where(i => i.AcademicYearID == acadyear).ToList();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public Tuition GetTuition(int tuitionID)
        {
            try
            {
                var entities = _context.Tuitions;
                return entities.Where(p => p.TuitionID == tuitionID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Tuition GetTuition(int acadyear, string lookupName)
        {
            try
            {
                var entities = _context.Tuitions;
                return entities.Where(p => p.AcademicYearID == acadyear && String.Compare(p.LookupName, lookupName, false) == 0).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddTuition(Tuition entity)
        {
            try
            {
                _context.Tuitions.Add(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateTuition(Tuition entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteTuition(int tuitionId)
        {
            Tuition entity = _context.Tuitions.Find(tuitionId);
            if (entity != null)
            {
                try
                {
                    _context.Tuitions.Remove(entity);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        #endregion

        #region "College Fees"
        public IEnumerable<CollegeFee> GetCollegeFees(int acadyear, bool includeUPass)
        {
            try
            {
                var entities = _context.CollegeFees;

                var fees = entities.Where(i => i.AcademicYearID == acadyear).ToList();

                if (!includeUPass)
                {
                    fees = fees.Where(a => a.IncludeOnCreateProgram == true).ToList();
                }

                return fees.ToList();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public CollegeFee GetCollegeFee(int collegeFeeID)
        {
            try
            {
                var entities = _context.CollegeFees;
                return entities.Where(p => p.CollegeFeeID == collegeFeeID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public CollegeFee GetCollegeFee(int acadyear, string lookupName)
        {
            try
            {
                var entities = _context.CollegeFees;
                return entities.Where(p => p.AcademicYearID == acadyear && String.Compare(p.LookupName, lookupName, false) == 0 ).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateCollegeFee(CollegeFee entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "Schools"
        public IEnumerable<School> GetSchools()
        {
            try
            {
                var schools = _context.Schools;
                return schools.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
