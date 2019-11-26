using System;
using System.Collections.Generic;
using System.Linq;
using Benday.Presidents.Core.Models;

namespace Benday.Presidents.Core.Services
{
    public interface IPresidentService
    {
        void Save(President saveThis);
        void DeletePresidentById(int id);
        President GetPresidentById(int id);
        IList<President> GetPresidents();
        IList<President> Search(
            string firstName, string lastName);
    }
}
