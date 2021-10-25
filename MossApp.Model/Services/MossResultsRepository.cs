using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MossApp.Data.Models;

namespace MossApp.Data.Services
{
    public class MossResultsRepository  : IMossResultsRepository, IDisposable
    {
        private MossResultsContext _context;

        public MossResultsRepository(MossResultsContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public void AddResults(Results result)
        {
            _context.Results.Add(result);
        }

        public async Task<Results> GetResultsAsync(int id)
        {
            return await _context.Results.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Results>> GetAllResultsAsync()
        {
            return await _context.Results.ToListAsync();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }

            }
        }
    }
}
