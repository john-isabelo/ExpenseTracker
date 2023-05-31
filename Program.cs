﻿/*+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+.
 * FILE         : Program.cs
 * PROJECT      : ExpenseTracker.cs
 * PROGRAMMER   : John Isabelo Aldeguer
 * FIRST VERSION: 2023-05-12
 * DESCRIPTION  :
 *      This program is tracking expense
 *+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-*/

using System.Globalization;

Dictionary<DateTime, decimal> expenses = LoadExpensesFromFile(out decimal totalExpenseThisMonth);
//Dictionary<DateTime, decimal> expenses = LoadExpensesFromFile();
DateTime currentDate = DateTime.Now.Date;

// Get yesterday's date
DateTime yesterday = currentDate.AddDays(-1);

// Get the expenses for yesterday
decimal yesterdayExpenses = expenses.GetValueOrDefault(yesterday);

Console.WriteLine("Expense Tracker");
Console.WriteLine("------------------------------------------------------");
Console.WriteLine("Today's Date: " + currentDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
Console.WriteLine("Yesterday's Expenses: $" + yesterdayExpenses);
Console.WriteLine();

Console.Write("Enter today's expenses: ");
if (decimal.TryParse(Console.ReadLine(), out decimal todayExpenses))
{
    // Add today's expenses to the dictionary
    expenses[currentDate] = todayExpenses;

    // Save the expenses to a text file
    SaveExpensesToFile(expenses);

    Console.WriteLine("Expenses saved successfully!");
}
else
{
    Console.WriteLine("Invalid expense amount. Expenses not saved.");
}

Console.WriteLine();

// Check the contents of the last saved text file
CheckLastSavedExpenses();
CheckVersion();

//Console.WriteLine("Press any key to exit.");
//Console.ReadKey();


static Dictionary<DateTime, decimal> LoadExpensesFromFile(out decimal totalExpenseThisMonth)
{
    string fileName = "expenses.txt";
    Dictionary<DateTime, decimal> expenses = new Dictionary<DateTime, decimal>();
    totalExpenseThisMonth = 0;

    try
    {
        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('\t');
                if (parts.Length == 2 && DateTime.TryParseExact(parts[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) && decimal.TryParse(parts[1], out decimal amount))
                {
                    expenses[date] = amount;

                    // Calculate total expense for the current month
                    if (date.Month == DateTime.Today.Month && date.Year == DateTime.Today.Year)
                    {
                        totalExpenseThisMonth += amount;
                    }
                }
            }
        }
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("Expense file not found. Starting with an empty expense list.");
    }

    return expenses;
}

//static Dictionary<DateTime, decimal> LoadExpensesFromFile()
//{
//    string fileName = "expenses.txt";
//    Dictionary<DateTime, decimal> expenses = new Dictionary<DateTime, decimal>();

//    try
//    {
//        using (StreamReader reader = new StreamReader(fileName))
//        {
//            string line;
//            while ((line = reader.ReadLine()) != null)
//            {
//                string[] parts = line.Split('\t');
//                if (parts.Length == 2 && DateTime.TryParseExact(parts[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) && decimal.TryParse(parts[1], out decimal amount))
//                {
//                    expenses[date] = amount;
//                }
//            }
//        }
//    }
//    catch (FileNotFoundException)
//    {
//        Console.WriteLine("Expense file not found. Starting with an empty expense list.");
//    }

//    return expenses;
//}

static void SaveExpensesToFile(Dictionary<DateTime, decimal> expenses)
{
    DateTime nextMonth = DateTime.Today.AddMonths(1);
    string fileName = $"expenses_{nextMonth:yyyy-MM}.txt";
    decimal totalExpense = 0;
    int currentMonth = -1;

    using (StreamWriter writer = new StreamWriter(fileName))
    {
        foreach (var expense in expenses)
        {
            if (expense.Key.Month != currentMonth)
            {
                if (currentMonth != -1)
                {
                    writer.WriteLine($"\t\t\tTotal Expense for {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(currentMonth)}: {totalExpense.ToString("C")}");
                    totalExpense = 0;
                }
                currentMonth = expense.Key.Month;
            }

            writer.WriteLine($"{expense.Key.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}\t{expense.Value}");
            totalExpense += expense.Value;
        }
        writer.WriteLine("------------------------------------------------------");
        writer.Write($"Total Expense for {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(currentMonth)}: {totalExpense.ToString("C")}");
    }
}

//static void SaveExpensesToFile(Dictionary<DateTime, decimal> expenses)
//{
//    string fileName = "expenses.txt";
//    decimal totalExpense = 0;
//    int currentMonth = -1;

//    using (StreamWriter writer = new StreamWriter(fileName))
//    {
//        foreach (var expense in expenses)
//        {
//            if (expense.Key.Month != currentMonth)
//            {
//                if (currentMonth != -1)
//                {
//                    writer.WriteLine($"\t\t\tTotal Expense for {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(currentMonth)}: {totalExpense.ToString("C")}");
//                    totalExpense = 0;
//                }
//                currentMonth = expense.Key.Month;
//            }

//            writer.WriteLine($"{expense.Key.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}\t{expense.Value}");
//            totalExpense += expense.Value;
//        }
//        writer.WriteLine("------------------------------------------------------");
//        writer.Write($"Total Expense for {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(currentMonth)}: {totalExpense.ToString("C")}");
//    }

//}

static void CheckLastSavedExpenses()
{
    string fileName = "expenses.txt";
    string userInput = "";

    Console.WriteLine("Checking last saved expenses...");
    Console.WriteLine();

    try
    {
        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("Expense file not found. No expenses saved yet.");
    }
    Console.WriteLine("------------------------------------------------------");
    Console.Write("Do you want to check the change logs? ");
    userInput = Console.ReadLine();

    if (userInput.ToLower() == "y")
    {
        CheckVersion();
    }
    else
    {
        Environment.Exit(0);
    }
    
}

static void CheckVersion()
{
    Console.Clear();
    Console.WriteLine("------------------------------------------------------");
    Console.WriteLine("Welcome to the change logs"); 
    Console.WriteLine("------------------------------------------------------\n");

    while (true)
    {
        Console.Write("Enter a command (or 'end' to exit): ");
        string userInput = Console.ReadLine();

        if (userInput.ToLower() == "end" || userInput.ToLower() == "exit")
        {
            break;
        }
        else
        {
            Console.WriteLine("Executing command: " + userInput);
            // Add your code logic here for processing the user's command
            ChangeLogs();
        }
    }
}

static void ChangeLogs()
{
    Console.Clear();
    Console.WriteLine("------------------------------------------------------");
    Console.WriteLine("\t\t\tChange logs");
    Console.WriteLine("------------------------------------------------------");
    Console.WriteLine("May 12, 2023: First code create");
    Console.WriteLine("May 26, 2023: Adding the change logs");
    Console.WriteLine("May 31, 2023: Automatic create a new text ");
    Console.WriteLine("            : file every month ");
    Console.WriteLine("------------------------------------------------------");
    Console.WriteLine("\n");
}