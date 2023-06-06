using Microsoft.EntityFrameworkCore;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.Fixtures
{
    public class StoreContextFixture : IDisposable
    {
        public StoreContext Context { get; set; }
        public StoreContextFixture()
        {
            var options = new DbContextOptionsBuilder<StoreContext>().UseSqlServer("").Options;
            Context = new StoreContext(options);
        }
        public void Dispose()
            => Context.Dispose();
    }
}
