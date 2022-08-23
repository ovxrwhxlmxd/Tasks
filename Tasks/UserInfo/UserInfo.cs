namespace Tasks.UserInfoNameSpace
{
    public class UserInfo
    {
        private IEnumerable<User> _users { get; set; }

        public UserInfo()
        {
            FillData();
        }

        private void FillData()
        {
            var users = new List<User>();
            users.Add(new User
            {
                Name = "Polo",
                Surname = "Reyhan",
                Age = new Random((int)DateTime.Now.Ticks).Next(15, 66)
            });
            users.Add(new User
            {
                Name = "Kersti",
                Surname = "Miĉjo",
                Age = new Random((int)DateTime.Now.Ticks).Next(15, 66)
            });
            users.Add(new User
            {
                Name = "Régulo",
                Surname = "Ailbe",
                Age = new Random((int)DateTime.Now.Ticks).Next(15, 66)
            });
            users.Add(new User
            {
                Name = "Jian",
                Surname = "Lukas",
                Age = new Random((int)DateTime.Now.Ticks).Next(15, 66)
            });
            users.Add(new User
            {
                Name = "Anja",
                Surname = "Ilaria",
                Age = new Random((int)DateTime.Now.Ticks).Next(15, 66)
            });
            _users = users;
        }

        /// <summary>
        /// Получить список пользователей по заданному фильтру
        /// </summary>
        /// <param name="filter">Функция для фильтрации</param>
        /// <returns>Список пользователей</returns>
        public IEnumerable<User> GetUsersWithFilter(Func<User, bool> filter)
        {
            return _users.Where(filter).ToList();
        }

        /// <summary>
        /// Получить список пользователей по названии поля и по значению
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="fieldName">Название поля</param>
        /// <param name="value">Значение</param>
        /// <param name="filterType">Тип фильтра</param>
        /// <returns></returns>
        public IEnumerable<User> GetUsersByFieldName<T>(string fieldName, T value, FilterType filterType)
            where T : IComparable
        {
            if (fieldName is null)
            {
                throw new Exception("Поле не может быть пустым");
            }
            if (typeof(User).GetField(fieldName)?.FieldType != value?.GetType())
            {
                throw new Exception("Тип значения не соответствует типу поля");
            }

            switch (filterType)
            {
                case FilterType.Equals:
                    return GetUsersWithFilter(u => value.CompareTo(typeof(User).GetField(fieldName).GetValue(u)) == 0);
                case FilterType.NotEquals:
                    return GetUsersWithFilter(u => value.CompareTo(typeof(User).GetField(fieldName).GetValue(u)) != 0);
                case FilterType.Contains:
                    return GetUsersWithFilter(u => typeof(User).GetField(fieldName).GetValue(u).ToString().Contains(value.ToString()));
                case FilterType.GreaterThan:
                    return GetUsersWithFilter(u => value.CompareTo(typeof(User).GetField(fieldName).GetValue(u)) < 0);
                case FilterType.LessThan:
                    return GetUsersWithFilter(u => value.CompareTo(typeof(User).GetField(fieldName).GetValue(u)) > 0);
                default:
                    return null;
            }
        }

        public class User
        {
            public string Name;
            public string Surname;
            public int Age;
        }

        public enum FilterType
        {
            Equals,
            NotEquals,
            Contains,
            GreaterThan,
            LessThan
        }
    }
}
