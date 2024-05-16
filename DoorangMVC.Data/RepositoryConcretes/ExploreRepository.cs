using DoorangMVC.Core.Models;
using DoorangMVC.Core.RepositoryAbstract;
using DoorangMVC.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorangMVC.Data.RepositoryConcretes
{
    public class ExploreRepository : GenericRepository<Explore>, IExploreRepository
    {
        public ExploreRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
