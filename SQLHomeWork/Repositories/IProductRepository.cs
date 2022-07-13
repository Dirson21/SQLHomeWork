using SQLHomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLHomeWork.Repositories
{
    interface IProductRepository
    {
        Product GetByName(string name);
    }
}
