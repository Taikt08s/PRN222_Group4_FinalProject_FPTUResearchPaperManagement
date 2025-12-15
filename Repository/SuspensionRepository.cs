using BusinessObject.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SuspensionRepository : ISuspensionRepository
    {
        private readonly AppDbContext _context;

        public SuspensionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> IsStudentSuspendedAsync(Guid studentId) { 
            return await _context.SuspensionRecords.
                AnyAsync(s => s.Student_Id == studentId && 
                (s.End_Date == null || s.End_Date > DateTime.UtcNow)); 
        }

        public async Task AddSuspensionAsync(SuspensionRecord record) { 
            _context.SuspensionRecords.Add(record); 
            await _context.SaveChangesAsync(); 
        }
    }
}
