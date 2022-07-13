using SQLHomeWork.Models;

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
