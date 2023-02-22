using System.Reflection;

namespace NetDelegatsUsingApp
{
    public delegate void Message(string message);

    public delegate T Operation<T>(T a, T b);
    internal class Program
    {
        static void Main(string[] args)
        {

            AccountExample();

        }

        static T Calc<T>(T a, T b, Operation<T> op)
        {
            return op(a, b);
        }
        static void AccountExample()
        {
            Account account1 = new(1000) { Name = "Bob" };
            account1.Notify += ConsoleMessage;
            account1.Notify += ConsoleColorMessage;

            account1.Put(200);
            account1.Take(500);
            account1.Take(600);

            account1.Notify -= ConsoleMessage;

            account1.Take(300);

            Console.WriteLine();

            Account account2 = new(1000) { Name = "Joe" };
            account2.Notify += ConsoleMessage;
            account2.Notify += ConsoleColorMessage;

            account2.Put(200);
            account2.Take(500);
            account2.Take(600);

            account2.Notify -= ConsoleMessage;

            account2.Take(300);


            void ConsoleMessage(Account sender, AccountEventArgs e)
            {
                Console.WriteLine($"Info for account: {sender.Name}");
                Console.WriteLine(e.Message);
                Console.WriteLine($"Amount put: {e.Amount}");
                Console.WriteLine($"Total account amount: {e.TotalAmount}");
            }

            void ConsoleColorMessage(Account sender, AccountEventArgs e)
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine($"Info for account: {sender.Name}");
                Console.WriteLine(e.Message);
                Console.WriteLine($"Amount put: {e.Amount}");
                Console.WriteLine($"Total account amount: {e.TotalAmount}");

                Console.ForegroundColor = oldColor;
            }
        }

        static void AnonimMetodsExample()
        {
            Message? message1 = delegate (string message)
            {
                Console.WriteLine(message);
            };

            Message? message2 = (message) =>
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ForegroundColor = oldColor;
            };

            message1?.Invoke("Hello world");

            message2 -= (message) =>
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ForegroundColor = oldColor;
            };


            Message? messages = new(message1);
            messages += message2;

            messages("AAAAAAAA");

            messages -= message1;

            messages("BBBBBBBB");


            message2?.Invoke("Good by world");

            int result = Calc(10, 20, delegate (int a, int b)
            {
                return a + b;
            });



            result = Calc(10, 20, (a, b) => a * b);
        }
    }
}