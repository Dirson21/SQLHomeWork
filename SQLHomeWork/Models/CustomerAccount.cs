
namespace SQLHomeWork.Models
{
    public class CustomerAccount
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }

        public CustomerAccount(int id, string login, string password, decimal balance)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentException($"\"{nameof(login)}\" не может быть неопределенным или пустым.", nameof(login));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"\"{nameof(password)}\" не может быть неопределенным или пустым.", nameof(password));
            }
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException($"\"{nameof(id)}\" не может быть меньше нуля.", nameof(id));
            }
            if (balance < 0)
            {
                throw new ArgumentOutOfRangeException($"\"{nameof(balance)}\" не может быть меньше нуля.", nameof(balance));
            }

            Id = id;
            Login = login;
            Password = password;
            Balance = balance;
        }

        public void UpdateLogin(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException($"\"{nameof(email)}\" не может быть неопределенным или пустым.", nameof(email));
            }

            Login = email;
        }
        public void UpdatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"\"{nameof(password)}\" не может быть неопределенным или пустым.", nameof(password));
            }

            Password = password;
        }
        public void UpdateBalance(decimal balance)
        {
            if (balance < 0)
            {
                throw new ArgumentOutOfRangeException($"\"{nameof(balance)}\" не может быть меньше нуля.", nameof(balance));
            }
        
            Balance = balance;
        }

    }
}
