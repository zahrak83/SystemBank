using SystemBank.Entities;
using SystemBank.Services;
using LibrarySystem.Extention; 

namespace SystemBank.ConsoleApp
{
    class Program
    {
        static CardService cardService = new CardService();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Welcome to SystemBank ===");

                Console.Write("Card Number: ");
                string cardNumber = (Console.ReadLine() ?? "").Trim();

                Console.Write("Password: ");
                string password = Console.ReadLine() ?? "";

                try
                {
                    var loggedCard = cardService.Authenticate(cardNumber, password);
                    AccountMenu(loggedCard);
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                    Console.ReadKey();
                    continue;
                }
            }
        }

        static void AccountMenu(Card card)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Logged in as: {card.CardNumber}");
                Console.WriteLine("1) Transfer Money");
                Console.WriteLine("2) Show Transactions");
                Console.WriteLine("3) Logout");
                Console.Write("Choose option: ");
                var opt = Console.ReadLine()?.Trim();

                switch (opt)
                {
                    case "1":
                        TransferFlow(card);
                        break;
                    case "2":
                        ShowTransactionsFlow(card);
                        break;
                    case "3":
                        return; 
                    default:
                        ShowError("Invalid option.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void TransferFlow(Card sourceCard)
        {
            Console.Clear();
            Console.WriteLine("=== Transfer Money ===");
            Console.WriteLine($"Source Card: {sourceCard.CardNumber}");

            Console.Write("Destination Card Number: ");
            string dest = (Console.ReadLine() ?? "").Trim();

            Console.Write("Amount to transfer: ");
            string amountStr = Console.ReadLine() ?? "";

            if (!float.TryParse(amountStr, out float amount))
            {
                ShowError("Invalid amount.");
                Console.ReadKey();
                return;
            }

            try
            {
                sourceCard = cardService.Transfer(sourceCard.CardNumber, dest, amount);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Transfer successful.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }

            Console.ReadKey();
        }

        static void ShowTransactionsFlow(Card card)
        {
            Console.Clear();
            Console.WriteLine($"=== Transactions for {card.CardNumber} ===");

            try
            {
                var txs = cardService.GetTransactions(card.CardNumber);
                if (txs.Count == 0)
                {
                    Console.WriteLine("(no transactions found)");
                }
                else
                {
                    ConsolePainter.WriteTable(txs, headerColor: ConsoleColor.Yellow, rowColor: ConsoleColor.White);
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }

            Console.ReadKey();
        }

        static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + message);
            Console.ResetColor();
        }
    }
}

