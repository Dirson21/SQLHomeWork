using SQLHomeWork.Models;

namespace SQLHomeWork.Repositories
{
    interface IProductRepository
    {
        Product GetByName(string name);
    }
}
