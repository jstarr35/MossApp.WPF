using MossApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MossApp.Data.Services
{
    public interface IMossResultsRepository
    {
        Task<IEnumerable<Results>> GetAllResultsAsync();
        Task<Results> GetResultsAsync(int id);
        void AddResults(Results results);
        bool Save();
    }
}
