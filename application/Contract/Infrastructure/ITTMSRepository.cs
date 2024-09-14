using domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Contract.Infrastructure
{
    public interface ITTMSRepository
    {
        Task AddRangeAsync(IEnumerable<TTMS> rows);
    }
}
