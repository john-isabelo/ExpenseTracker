/*
// Get today's and yesterday's expenses
double todayExpense = GetExpense("Enter today's expense: ");
double yesterdayExpense = GetExpense("Enter yesterday's expense: ");

// Calculate the percentage difference
double percentageDifference = CalculatePercentageDifference(todayExpense, yesterdayExpense);

// Display the result
Console.WriteLine($"The percentage difference between yesterday's and today's expenses is: {percentageDifference}%");

static double GetExpense(string message)
{
    Console.Write(message);
    string expenseInput = Console.ReadLine();
    double expense;
    while (!double.TryParse(expenseInput, out expense))
    {
        Console.WriteLine("Invalid input. Please enter a valid expense amount: ");
        expenseInput = Console.ReadLine();
    }
    return expense;
}

static double CalculatePercentageDifference(double currentExpense, double previousExpense)
{
    double difference = currentExpense - previousExpense;
    double percentageDifference = (difference / previousExpense) * 100;
    return Math.Round(percentageDifference, 2); // Round to two decimal places
}
*/

/*
List<string> messages = new List<string>();

Console.WriteLine("Enter messages (enter 'exit' to finish):");

// Read messages from the user
string userInput;
do
{
    userInput = Console.ReadLine();
    if (userInput != "exit")
    {
        messages.Add(userInput);
    }
} while (userInput != "exit");

string fileName = "messages.txt";

// Write messages to the file
File.WriteAllLines(fileName, messages);

Console.WriteLine("Messages saved to file: " + fileName);
Console.WriteLine();

// Read the content from the file
string[] fileContent = File.ReadAllLines(fileName);

Console.WriteLine("Content read from file:");
foreach (string line in fileContent)
{
    Console.WriteLine(line);
}
*/