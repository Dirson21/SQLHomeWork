
using SQLHomeWork.Models;
using SQLHomeWork.Repositories;

const string connectionString = @"Data Source=ZALMAN\SQLEXPRESS;Initial Catalog=Pizzeria;Pooling=true;Integrated Security=SSPI;TrustServerCertificate=True";

ICustomerAccountRepository customerRepository = new RawSqlCustomerAccountRepository(connectionString);
IProductRepository productRepository = new RawSqlProductRepository(connectionString);

PrintCommands();

while(true)
{
    Console.WriteLine("Введите команду");
    string command = Console.ReadLine();

    if (command == "get-customer")
    {
        IReadOnlyList<CustomerAccount> customers = customerRepository.GetAll();
        if (customers.Count == 0)
        {
            Console.WriteLine("Покупатели не найдены");
            continue;
        }
        foreach(var customer in customers)
        {
            Console.WriteLine($"Id: {customer.Id}, Login: {customer.Login}, Password: {customer.Password}, Balance: {customer.Balance}");
        }
    }
    else if (command == "get-product-by-name")
    {

        while (true)
        {
            Console.WriteLine("Введите навзание товара");
            string pizzaName = Console.ReadLine();
            if (pizzaName == "exit")
            {
                break;
            }
            if (string.IsNullOrEmpty(pizzaName))
            {
                Console.WriteLine("Неккоректное название");
                Console.WriteLine("Повторите ввод или введите exit");
                continue;
            }

            Product product = productRepository.GetByName(pizzaName);
            if (product == null)
            {
                Console.WriteLine("Данного товара нет в наличии");
                Console.WriteLine("Повторите ввод или введите exit");
                continue;
            }

            Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}");
            break;
        }

    }
    else if (command == "get-customer-totalprice")
    {
        IReadOnlyList<Tuple<CustomerAccount, decimal>> customers = customerRepository.GetAllTotalPrice();
        if (customers.Count == 0)
        {
            Console.WriteLine("Покупателей, заказавших товар, не найдено");
            continue;
        }
        foreach(var customer in customers)
        {
            Console.WriteLine($"Id:{customer.Item1.Id}, Login: {customer.Item1.Login}, TotalPrice: {customer.Item2} ");
        }
    }
    else if (command == "delete-costumer-by-id")
    {
        while (true)
        {

            Console.WriteLine("Введите id покупателя");
            string idCommand = Console.ReadLine();
            if (idCommand == "exit")
            {
                break;
            }
            int id;
            if (!int.TryParse(idCommand, out id))
            {
                Console.WriteLine("Неккоректный id");
                Console.WriteLine("Повторите ввод или введите exit");
                continue;
            }

            CustomerAccount customer = customerRepository.GetById(id);
            if (customer == null)
            {
                Console.WriteLine("Покупатель с данным id отсутсвует");
                Console.WriteLine("Повторите ввод или введите exit");
                continue;
            }
            customerRepository.Delete(customer);
            Console.WriteLine("Удаление прошло успешно");
            break;

        }
    }
    else if (command == "update-customer")
    {
        while (true)
        {
            Console.WriteLine("Введите id покупателя");
            string idCommand = Console.ReadLine();
            if (idCommand == "exit")
            {
                break;
            }
            int id;
            if (!int.TryParse(idCommand, out id))
            {
                Console.WriteLine("Неккоректный id");
                Console.WriteLine("Повторите ввод или введите exit");
                continue;
            }

            CustomerAccount customer = customerRepository.GetById(id);
            if (customer == null)
            {
                Console.WriteLine("Покупатель с данным id отсутсвует");
                Console.WriteLine("Повторите ввод или введите exit");
                continue;
            }
            Console.WriteLine("Введите новый логин");
            string login = Console.ReadLine();
            if (string.IsNullOrEmpty(login))
            {
                Console.WriteLine("Неккоректный логин");
                Console.WriteLine("Повторите ввод или введите exit");
                continue;
            }
            customer.UpdateLogin(login);
            Console.WriteLine("Введите новый пароль");
            string password = Console.ReadLine();
            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Неккоректный пароль");
                Console.WriteLine("Повторите ввод или введите exit");
                continue;
            }
            customer.UpdatePassword(password);

            Console.WriteLine("Введите новый баланс");
            string balanceCommand = Console.ReadLine();
            
            decimal balance;
            if (!decimal.TryParse(balanceCommand, out balance))
            {
                Console.WriteLine("Неккоректный balance");
                Console.WriteLine("Повторите ввод или введите exit");
                continue;
            }
            if (balance < 0)
            {
                Console.WriteLine("Balance не может быть меньше нуля");
                continue;
            }
            customer.UpdateBalance(balance);
            customerRepository.Update(customer);
            break;
        }
    }
    else if (command == "help")
    {
        PrintCommands();
    }
    else if (command == "exit")
    {
        break;
    }
}







void PrintCommands()
{
    Console.WriteLine("Доступные команды:");
    Console.WriteLine("get-customer - Получить список всех покупателей");
    Console.WriteLine("get-customer-totalprice - получить список покупателей, которые заказали товар, и сумму заказа");
    Console.WriteLine("get-product-by-name - Получить товар по имени");
    Console.WriteLine("delete-costumer-by-id - Удалить покупателя по id");
    Console.WriteLine("update-customer - Обновить покупателя");
    Console.WriteLine("help - Список команд");
    Console.WriteLine("exit - Выход");
}
