using SQLHomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLHomeWork.Repositories
{
    interface ICustomerAccountRepository
    {
        IReadOnlyList<CustomerAccount> GetAll();
        CustomerAccount GetByLogin(string login);
        CustomerAccount GetById(int id);
        void Update(CustomerAccount customerAccount);
        void Delete(CustomerAccount customerAccount);

        IReadOnlyList<Tuple<CustomerAccount, decimal>> GetAllTotalPrice();




    }
}
