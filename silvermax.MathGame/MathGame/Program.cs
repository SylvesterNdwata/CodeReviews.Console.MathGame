using System.Diagnostics;

Console.WriteLine("Welcome to the Math Game.");
Console.WriteLine("How many questions would you like to answer?");
var readResult = Console.ReadLine();
var questionNumber = 0;
if (readResult != null)
{
    if (!int.TryParse(readResult, out questionNumber))
    {
        throw new FormatException(message: "Please input a valid integer");
    }
}

var points = 0;

Random rand = new Random();

List<string> correctQuestions = new();
List<string> wrongQuestions = new();

//Stopwatch sw = Stopwatch.StartNew();

bool gameContinue = true;

while (gameContinue)
{
    Console.WriteLine(@"Please Select an Operation:
    1. Addition(+)
    2. Subtraction(-)
    3. Multiplication(x)
    4. Division(/)
    5. Choose this option to see how many points you have.
    6. Choose this option to see the questions that were done.
    7. Choose the option to exit the game.");

    readResult = Console.ReadLine();
    Console.Clear();

    if (readResult != null)
    {
        switch (readResult)
        {
            case "1":

                Game(Addition, "+", questionNumber);
                break;

            case "2":

                Game(Subtraction, "-", questionNumber);
                break;

            case "3":
                Game(Multiplication, "x", questionNumber);
                break;

            case "4":
                Game(Division, "/", questionNumber);
                break;

            case "5":
                Console.WriteLine($"You have {points} points");
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                Console.Clear();
                break;

            case "6":
                printQuestionHistory();
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                Console.Clear();
                break;

            case "7":
                gameContinue = false;
                break;

            default:
                Console.WriteLine("Please enter a valid choice!");
                break;
        }
    }
}

//Console.WriteLine("Here are the questions you answered:");
//foreach (string qn in completedQuestions)
//{
//    Console.WriteLine(qn);
//}

//sw.Stop();
//Console.WriteLine($"You took {sw.Elapsed.TotalMinutes} minutes to finish the game");

void Game(Func<int, int, int> operation, string opSign, int repitition)
{
    Stopwatch sw = Stopwatch.StartNew();

    for (int i = 0; i <= repitition + 1; i++)
    {
        var num1 = rand.Next(1, 101);
        var num2 = rand.Next(1, 101);

        if (opSign.Equals("/"))
        {
            do
            {
                num1 = rand.Next(1, 101);
                num2 = rand.Next(1, 101);
            } while (num1 % num2 != 0);
        }

        var result = operation(num1, num2);

        Console.WriteLine($"What is {num1} {opSign} {num2}?");
        var readResult = Console.ReadLine();

        if (readResult != null)
        {
            if (!int.TryParse(readResult, out int answer))
            {
                Console.WriteLine("Please input a valid integer answer");
                return;
            }

            if (answer == result)
            {
                points++;
                correctQuestions.Add($"{num1} {opSign} {num2} = {result}");
            }
            else
            {
                Console.WriteLine($"You got the question wrong, the answer is {result}");
                wrongQuestions.Add($"{num1} {opSign} {num2} = {answer}. The correct answer was {result}");

            }
        }

        
        Console.Clear();
    }

    //Console.WriteLine("Here are the questions you answered correctly:");
    //foreach (string qn in correctQuestions)
    //{
    //    Console.WriteLine(qn);
    //}
    //Console.WriteLine();

    //Console.WriteLine("Here are the questions you answered incorrectly:");
    //foreach (string qn in wrongQuestions)
    //{
    //    Console.WriteLine(qn);
    //}
    //Console.WriteLine();
    printQuestionHistory();

    sw.Stop();
    Console.WriteLine($"You took {sw.Elapsed.TotalMinutes} minutes to finish the game");
    Console.WriteLine("\nPress Enter to continue...");
    Console.ReadLine();
    Console.Clear();
}

int Addition(int num1, int num2) => num1 + num2;

int Subtraction(int num1, int num2) => num1 - num2;

int Multiplication(int num1, int num2) => num1 * num2;

int Division(int num1, int num2) => num1 / num2;

void printQuestionHistory()
{
    Console.WriteLine("Here are the questions you answered correctly:");
    foreach (string qn in correctQuestions)
    {
        Console.WriteLine(qn);
    }
    Console.WriteLine();

    Console.WriteLine("Here are the questions you answered incorrectly:");
    foreach (string qn in wrongQuestions)
    {
        Console.WriteLine(qn);
    }
    Console.WriteLine();
}