using Tasks.ATM;
using Tasks.CircularLinkedList;
using Tasks.Football;
using Tasks.Reflection;
using Tasks.UserInfoNameSpace;
using static Tasks.ValidationAttributes.ValidationAttributes;

namespace Tasks
{
    public class TaskChecker
    {
        /// <summary>
        /// Запустить выполнения задания
        /// </summary>
        /// <param name="taskNumber">Номер задания</param>
        /// <returns>True если задача выполнена, false - нет</returns>
        public static bool StartTask(int taskNumber)
        {
            var result = taskNumber > 0 && taskNumber <= 40;
            if (result)
            {
                switch (taskNumber)
                {
                    case 1: Task1(); break;
                    case 2: Task2(); break;
                    case 3: Task3(); break;
                    case 4: Task4(); break;
                    case 5: Task5(); break;
                    case 6: Task6(); break;
                    case 7: Task7(); break;
                    case 8: Task8(); break;
                    case 9: Task9(); break;
                    case 10: Task10(); break;
                    case 11: Task11(); break;
                    case 12: Task12(); break;
                }
            }
            return result;
        }

        #region 1 Задание
        private static void Task1()
        {
            var team = new SoccerTeam();

            team[10] = new SoccerPlayer
            {
                Name = "Ten",
                Number = 10
            };

            var player = team[5];
        }
        #endregion

        #region 2 Задание
        private static void Task2()
        {
            var list = new CircularLinkedList<int>();
            list.AddRange(new int[] { 4, 6, 3, 5, 9, 4, 1, 0, 2 });
            list.Remove(4);

            if (!list.IsEmpty)
            {
                list.Clear();
            }
        }
        #endregion

        #region 3 Задание
        private static void Task3()
        {
            var list = new CircularLinkedList<int>();
            list.AddRange(new int[] { 4, 6, 3, 5, 9, 4, 1, 0, 2 });

            var count = 0;
            foreach (var item in list)
            {
                count++;
            }
        }
        #endregion

        #region 4 Задание
        private static void Task4()
        {
            var list = new CircularLinkedList<int>();
            list.AddRange(new int[] { 4, 6, 3, 5, 9, 4, 1, 0, 2 });
            list.Order();
        }
        #endregion

        #region 5 Задание
        private static void Task5()
        {
            var ATM = ATMWorker.Instance;

            var user = ATM.GetMyAccountInfo("PoloReyhan", "PoloReyhan");
            var accounts = ATM.GetAccountsByUserId(user.Id);
            var accountsWithHistory = ATM.GetAccountsWithHistoryByUserId(user.Id);
            var receiptHistory = ATM.GetReciptHistoryWithUserInfo();
            var usersWithBalance = ATM.GetUsersWithBalanceGTValue(6);
        }
        #endregion

        #region 6 Задание
        private static void Task6()
        {
            var links = LinkParser.LinkParser.GetLinks("https://html5book.ru/hyperlinks-in-html/");
        }
        #endregion

        #region 7 Задание
        private static void Task7()
        {
            var person = new Person
            {
                Name = "Sandra",
                Surname = "Gréta",
                MiddleName = "Narcís"
            };
            person.InitializeSumFunctions();
        }
        #endregion

        #region 8 Задание
        private static void Task8()
        {
            var person = new Person
            {
                Name = "Sandra",
                Surname = "Gréta",
                MiddleName = "Narcís"
            };
            person.InitializeSumFunctions();
            person.TryCallAsyncWithReflection();
        }
        #endregion

        #region 9 Задание
        private static void Task9()
        {
            var userInfo = new UserInfo();

            var users = userInfo.GetUsersWithFilter(u => u.Age > 30);
        }
        #endregion

        #region 10 Задание
        private static void Task10()
        {
            var userInfo = new UserInfo();

            var usersWithNamePolo = userInfo.GetUsersByFieldName(nameof(UserInfo.User.Name), "Polo", UserInfo.FilterType.Equals);

            var usersWithAgeGT = userInfo.GetUsersByFieldName(nameof(UserInfo.User.Age), 30, UserInfo.FilterType.GreaterThan);
            var usersWithAgeLT = userInfo.GetUsersByFieldName(nameof(UserInfo.User.Age), 30, UserInfo.FilterType.LessThan);
        }
        #endregion

        #region 11 Задание
        private static void Task11()
        {
            var contact = new ContactInfo
            {
                Phone = "89379510709",
                Email = "hqqq@wqerqwr.com"
            };
        }
        #endregion

        #region 12 Задание
        private static void Task12()
        {
        }
        #endregion
    }
}
