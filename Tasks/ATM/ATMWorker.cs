namespace Tasks.ATM
{
    public class ATMWorker
    {
        private static ATMWorker _instance;

        private ATM _ATM { get; set; }

        private ATMWorker()
        {
            _ATM = new ATM();
        }

        public static ATMWorker Instance
        {
            get { return _instance ?? (_instance = new ATMWorker()); }
        }

        /// <summary>
        /// Получить информацию об аккаунте по лоигну и паролю
        /// </summary>
        public User GetMyAccountInfo(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                throw new Exception("Не заполнен логин или пароль");

            var user = _ATM.Users.FirstOrDefault(_ => _.Login.ToLower() == login.ToLower() && _.Password == password);

            if (user == null)
                throw new Exception("Пользователь не найден");
            else
                return user;
        }

        /// <summary>
        /// Получить информацию о счетах пользователя
        /// </summary>
        public IEnumerable<Account> GetAccountsByUserId(Guid userId)
        {
            return _ATM.Accounts.Where(_ => _.UserId == userId);
        }

        /// <summary>
        /// Получить информацию о счетах пользователя с историей операций
        /// </summary>
        public IEnumerable<Account> GetAccountsWithHistoryByUserId(Guid userId)
        {
            return GetAccountsByUserId(userId).Select(_ => { _.Histories = GetHistoriesByAccountId(_.Id); return _; });
        }

        /// <summary>
        /// Получить информацию о счетах пользователя с историей операций
        /// </summary>
        public IEnumerable<AccountWithHistoryDTO> GetAccountsWithHistoryByUserId2nd(Guid userId)
        {
            return GetAccountsByUserId(userId).SelectMany(a => _ATM.History.Where(h => a.Id == h.AccountId), (a, h) => new AccountWithHistoryDTO
            {
                Id = a.Id,
                UserId = a.UserId,
                AccountCreationDate = a.CreationDate,
                AccountBalance = a.Balance,
                OperationDate = h.OperationDate,
                OperationType = h.OperationType,
                Amount = h.Amount,
            });
        }

        /// <summary>
        /// Получить все операции пополнения c информацией пользователей
        /// </summary>
        public Dictionary<History, User> GetReciptHistoryWithUserInfo()
        {
            var history = _ATM.History.Where(_ => _.OperationType == OperationType.Receipt).Select(_ => { _.Account = GetAccountById(_.AccountId); return _; });

            return history.Join(_ATM.Users, outerKeySelector: h => h.Account.UserId, innerKeySelector: u => u.Id, (h, u) => new { History = h, User = u }).ToDictionary(key => key.History, value => value.User);
        }

        /// <summary>
        /// Получить пользователей с балансом больше чем значение
        /// </summary>
        /// <param name="value">Сумма для сравнения</param>
        public IEnumerable<User> GetUsersWithBalanceGTValue(decimal value)
        {
            return _ATM.Users.Select(_ => { _.Accounts = GetAccountsByUserId(_.Id); return _; }).Where(_ => _.Accounts.Any(a => a.Balance > value));
        }

        /// <summary>
        /// Получить пользователей с балансом больше чем значение
        /// </summary>
        /// <param name="value">Сумма для сравнения</param>
        public IEnumerable<User> GetUsersWithBalanceGTValue2nd(decimal value)
        {
            return _ATM.Users.Select(_ => { _.Accounts = GetAccountsByUserId(_.Id); return _; }).Where(_ => _.Accounts.Any(a => a.Balance > value));
        }

        private Account GetAccountById(Guid accountId)
        {
            return _ATM.Accounts.FirstOrDefault(_ => _.Id == accountId);
        }

        private IEnumerable<History> GetHistoriesByAccountId(Guid accountId)
        {
            return _ATM.History.Where(_ => _.AccountId == accountId);
        }

        private class ATM
        {
            /// <summary>
            /// Коллекция Пользователей - хранит информацию о польователях, зарегистрированных в банке
            /// </summary>
            public List<User> Users { get; set; } = new List<User>();

            /// <summary>
            /// Коллекция Счетов - хранит информацию о счетах польователей, у каждого пользователя может быть несколько счетов.
            /// </summary>
            public List<Account> Accounts { get; set; } = new List<Account>();

            /// <summary>
            /// Коллекция Историй операций - хранит истории о всех операциях с конкретным счётом, имется 2 типа операции, пополнение и вывод денег со счёта.
            /// </summary>
            public List<History> History { get; set; } = new List<History>();

            public ATM()
            {
                FillData();
            }

            private void FillData()
            {
                Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Polo",
                    SurName = "Reyhan",
                    PhoneNumber = "89993535555",
                    RegistrationDate = DateTime.Now,
                    Login = "PoloReyhan",
                    Password = "PoloReyhan"
                });
                Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Kersti",
                    SurName = "Miĉjo",
                    PhoneNumber = "89993536666",
                    RegistrationDate = DateTime.Now,
                    Login = "KerstiMiĉjo",
                    Password = "KerstiMiĉjo"
                });
                Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Régulo",
                    SurName = "Ailbe",
                    PhoneNumber = "89993537777",
                    RegistrationDate = DateTime.Now,
                    Login = "RéguloAilbe",
                    Password = "RéguloAilbe"
                });
                Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Jian",
                    SurName = "Lukas",
                    PhoneNumber = "89993538888",
                    RegistrationDate = DateTime.Now,
                    Login = "JianLukas",
                    Password = "JianLukas"
                });
                Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Anja",
                    SurName = "Ilaria",
                    PhoneNumber = "89993535555",
                    RegistrationDate = DateTime.Now,
                    Login = "AnjaIlaria",
                    Password = "AnjaIlaria"
                });

                foreach (var user in Users)
                {
                    var accountsCount = new Random((int)DateTime.Now.Ticks).Next(1, 4);
                    for (var i = 0; i < accountsCount; i++)
                        Accounts.Add(new Account
                        {
                            Id = Guid.NewGuid(),
                            UserId = user.Id,
                            CreationDate = DateTime.Now,
                            Balance = 0
                        });
                }

                foreach (var account in Accounts)
                {
                    var receiptCount = new Random((int)DateTime.Now.Ticks).Next(1, 5);
                    var writeoffCount = new Random((int)DateTime.Now.Ticks).Next(1, 3);

                    for (var i = 0; i < receiptCount; i++)
                    {
                        var amount = new Random((int)DateTime.Now.Ticks).Next(0, 300000);
                        History.Add(new History
                        {
                            Id = Guid.NewGuid(),
                            AccountId = account.Id,
                            OperationDate = DateTime.UtcNow,
                            OperationType = OperationType.Receipt,
                            Amount = amount
                        });
                        account.Balance += amount;
                    }
                    for (var i = 0; i < writeoffCount; i++)
                    {
                        var amount = new Random((int)DateTime.Now.Ticks).Next(0, 300000);
                        History.Add(new History
                        {
                            Id = Guid.NewGuid(),
                            AccountId = account.Id,
                            OperationDate = DateTime.UtcNow,
                            OperationType = OperationType.WriteOff,
                            Amount = amount
                        });
                        account.Balance -= amount;
                    }
                }
            }
        }

        public class User
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string SurName { get; set; }
            public string MiddleName { get; set; }
            public string PhoneNumber { get; set; }
            public DateTime RegistrationDate { get; set; }
            public string Login
            {
                get { return _login; }
                set
                {
                    if (string.IsNullOrEmpty(value))
                        throw new Exception("Логин не может быть пустым");
                    else
                        _login = value;
                }
            }
            private string _login;
            public string Password
            {
                get { return _password; }
                set
                {
                    if (string.IsNullOrEmpty(value))
                        throw new Exception("Пароль не может быть пустым");
                    else
                        _password = value;
                }
            }
            private string _password;

            public IEnumerable<Account> Accounts { get; set; }

        }

        public class Account
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public DateTime CreationDate { get; set; }
            public decimal Balance { get; set; }
            public IEnumerable<History> Histories { get; set; }
        }

        public class History
        {
            public Guid Id { get; set; }
            public Guid AccountId { get; set; }
            public Account Account { get; set; }
            public DateTime OperationDate { get; set; }
            public OperationType OperationType { get; set; }
            public decimal Amount { get; set; }
        }

        public enum OperationType
        {
            Receipt,
            WriteOff
        }

        public class AccountWithHistoryDTO
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public DateTime AccountCreationDate { get; set; }
            public decimal AccountBalance { get; set; }
            public DateTime OperationDate { get; set; }
            public OperationType OperationType { get; set; }
            public decimal Amount { get; set; }
        }
    }
}
