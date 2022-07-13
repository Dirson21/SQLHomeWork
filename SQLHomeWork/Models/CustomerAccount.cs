using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLHomeWork.Models
{
    public class CustomerAccount
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }

        public CustomerAccount(int id, string email, string password, decimal balance)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException($"\"{nameof(email)}\" не может быть неопределенным или пустым.", nameof(email));
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
            Email = email;
            Password = password;
            Balance = balance;
        }

        public void UpdateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException($"\"{nameof(email)}\" не может быть неопределенным или пустым.", nameof(email));
            }

            Email = email;
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
