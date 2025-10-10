using LibrarySystem.Extention;
using SystemBank.Interface.IService;
using SystemBank.Services;

namespace SystemBank
{
    public class Program
    {
        static void Main(string[] args)
        {
            ICardService cardService = new CardService();
            ITransactionService transactionService = new TransactionService();


            while (true)
            {
                Console.Clear();
                ConsolePainter.WriteLine("=== SystemBank Login ===", ConsoleColor.Cyan);

                Console.Write("Card Number: ");
                string cardNumber = Console.ReadLine()!;

                Console.Write("Password: ");
                string password = Console.ReadLine()!;

                var loginResult = cardService.Login(cardNumber, password);

                if (!loginResult.IsSuccess)
                {
                    ConsolePainter.WriteLine(loginResult.Message, ConsoleColor.Red);
                    Console.ReadKey();
                    continue;
                }

                bool logout = false;
                while (!logout) 
                {
                    Console.Clear();
                    ConsolePainter.WriteLine($"Logged in: {cardNumber}", ConsoleColor.Green);
                    ConsolePainter.WriteLine("=== User Menu ===", ConsoleColor.Cyan);
                    ConsolePainter.WriteLine("1. Transfer Money");
                    ConsolePainter.WriteLine("2. Show Transactions");
                    ConsolePainter.WriteLine("3. Change Password");
                    ConsolePainter.WriteLine("4. Exit");
                    Console.Write("Select option: ");
                    string option = Console.ReadLine()!;

                    switch (option)
                    {
                        case "1": 
                            Console.Clear();
                            ConsolePainter.WriteLine($"Your card: {cardNumber}", ConsoleColor.Yellow);
                            Console.Write("Destination Card Number: ");
                            string destCard = Console.ReadLine()!;
                            
                            string? holderName = cardService.GetHolderNameByCardNumber(destCard);

                            if (holderName == null)
                            {
                                ConsolePainter.WriteLine("Destination card not found.", ConsoleColor.Red);
                                Console.ReadKey();
                                break;
                            }

                            Console.WriteLine($"Destination card holder: {holderName}");

                            Console.Write("Amount: ");
                            bool validAmount = float.TryParse(Console.ReadLine(), out float amount);
                            if (!validAmount)
                            {
                                ConsolePainter.WriteLine("Invalid amount!", ConsoleColor.Red);
                                Console.ReadKey();
                                break;
                            }

                            var transferResult = transactionService.Transfer(cardNumber, destCard, amount);
                            if (transferResult.IsSuccess)
                                ConsolePainter.WriteLine(transferResult.Message, ConsoleColor.Green);
                            else
                                ConsolePainter.WriteLine(transferResult.Message, ConsoleColor.Red);

                            Console.ReadKey();
                            break;

                        case "2": 
                            Console.Clear();
                            var transactions = transactionService.GetAll(cardNumber);
                            ConsolePainter.WriteLine("=== Your Transactions ===", ConsoleColor.Cyan);
                            ConsolePainter.WriteTable(transactions, ConsoleColor.Yellow, ConsoleColor.White);
                            Console.ReadKey();
                            break;

                        case "3":
                            Console.Clear();
                            Console.Write("Current Password: ");
                            string currentPass = Console.ReadLine()!;
                            Console.Write("New Password: ");
                            string newPass = Console.ReadLine()!;

                            var changeRes = cardService.ChangePassword(cardNumber, currentPass, newPass);
                            if (changeRes.IsSuccess)
                                ConsolePainter.WriteLine(changeRes.Message, ConsoleColor.Green);
                            else
                                ConsolePainter.WriteLine(changeRes.Message, ConsoleColor.Red);

                            Console.ReadKey();
                            break;

                        case "4": 
                            logout = true;
                            break;

                        default:
                            ConsolePainter.WriteLine("Invalid option! Try again.", ConsoleColor.Red);
                            Console.ReadKey();
                            break;
                    }
                }
            }
        }
    }
}

