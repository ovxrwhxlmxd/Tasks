using System.Reflection;
using System.Runtime.CompilerServices;

namespace Tasks.Reflection
{
    public class Person
    {
        public string Name;
        public string Surname { get; set; }
        public string MiddleName { get; set; }

        private decimal _salary;

        public string GetPersonInfo()
        {
            var info = $"Я, {Name} {Surname} {MiddleName}, зарабатываю {_salary}";
            Console.WriteLine(info);
            return info;
        }

        public void GetPersonClassInfo()
        {
            var type = GetType();


            var typeInfo = type.GetTypeInfo();
            var members = type.GetMembers();
            var constructors = type.GetConstructors();
            var properties = type.GetProperties();
            var fields = type.GetFields();
            var methods = type.GetMethods();

            var getPersonInfoMethod = type.GetMethod("GetPersonInfo");
            getPersonInfoMethod.Invoke(this, null);

            var _salary = type.GetField("_salary", BindingFlags.Instance | BindingFlags.NonPublic);
            _salary.SetValue(this, 10m);
            getPersonInfoMethod.Invoke(this, null);
        }

        public void InitializeSumFunctions()
        {
            var type = typeof(SumFunctions);

            var constructor = type.GetConstructor(new Type[0]);
            var sumFunctions = constructor.Invoke(null);
            var sumFunctionsMethods = sumFunctions.GetType().GetMethods();

            foreach (var method in sumFunctionsMethods)
            {
                var parametrInfos = method.GetParameters();

                var parametrs = new Object[parametrInfos.Length];

                var step = 0;
                foreach (var info in parametrInfos)
                {
                    parametrs[step++] = new Random((int)DateTime.Now.Ticks).Next(1, 5);
                }
                method.Invoke(sumFunctions, parametrs);
            }
        }

        public void TryCallAsyncWithReflection()
        {
            var asyncSumFunction = typeof(AsyncSumFunction).GetConstructor(new Type[0]).Invoke(null);

            var methods = asyncSumFunction.GetType().GetMethods();

            foreach (var method in methods)
            {
                if (method.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) != null)
                {
                    var result = ((Task<int>)method.Invoke(asyncSumFunction, null)).GetAwaiter().GetResult();
                }
            }
        }

        class SumFunctions
        {
            public void Sum(int a, int b)
            {
                Console.WriteLine("Начало метода Sum2");
                Console.WriteLine(a + b);
                Console.WriteLine("Завершение метода Sum2");
            }
            public void Sum(int a, int b, int c)
            {
                Console.WriteLine("Начало метода Sum3");
                Console.WriteLine(a + b + c);
                Console.WriteLine("Завершение метода Sum3");
            }
            public void Sum(int a, int b, int c, int d)
            {
                Console.WriteLine("Начало метода Sum4");
                Console.WriteLine(a + b + c + d);
                Console.WriteLine("Завершение метода Sum4");
            }
        }

        class AsyncSumFunction
        {
            public async Task<int> SumAsync()
            {
                return await Task.Run(() => 100 + 100);
            }
        }
    }
}
