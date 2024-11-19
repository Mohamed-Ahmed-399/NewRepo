using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class clsMathGame
    {
        
        byte NumberOfQuestions;
        enum enLevel { Easy = 1 , Medium = 2 , Hard = 3 , Mix = 4};
        enum enOperation { Add = 1 , Sub = 2 , Mult = 3 , Div = 4 , Mix = 5};

        enLevel GameLevel { get; set; }
        enOperation Operation { get; set; }

        enum enResult {  Success = 1 , Failure = 2 };
        byte CorrectAnswersNumber = 0;
        byte WrongAnswersNumber = 0;

        byte ReadQuestionsNumber()
        {
            Console.Write("How many questions do you want to answer ?? ");
            byte Number = byte.Parse(Console.ReadLine());
            return NumberOfQuestions = Number;
        }
        
        enLevel ReadLevel()
        {
            Console.Write("Choose Level : [1] Easy , [2] Medium , [3] Hard , [4] Mix ?? ");
            byte Choose = byte.Parse(Console.ReadLine());
            while(Choose < 1 || Choose > 4)
            {
                Console.Write("Invalid Number , Enter Another Number ?? ");
                Choose = byte.Parse(Console.ReadLine());
            }
            return GameLevel = (enLevel) Choose;
        }

        enOperation ReadOperation()
        {
            Console.Write("Choose Operation : [1] Add , [2] Sub , [3] Mult , [4] Div , [5] Mix ?? ");
            byte Choose = byte.Parse(Console.ReadLine());
            while(Choose < 1 || Choose > 5)
            {
                Console.Write("Invalid Number , Enter Another Number ?? ");
                Choose = byte.Parse((Console.ReadLine()));
            }

            return Operation = (enOperation) Choose;
        }

        enResult FinalResult;
        class clsGameRound
        {
            enLevel Level { get; set; }
            enOperation Operation { get; set; }

            
            int FirstNumber { get; set; }
            int LastNumber { get; set; }
            int PlayerAnswer;
            int CorrectAnswer;
            public bool IsCorrect;

            int ReadPlayerAnswer()
            {
                int Answer = int.Parse(Console.ReadLine()) ;
                return PlayerAnswer = Answer;
            }

            public clsGameRound(enLevel level, enOperation operation)
            {
                Random random1 = new Random();
                
                Level = (level == enLevel.Mix) ? (enLevel)(random1.Next(1, 5)) : level;
                Operation = (operation == enOperation.Mix) ? (enOperation)(random1.Next(1, 6)) : operation;

                if(Level == enLevel.Easy)
                {
                    FirstNumber = random1.Next(1, 11);
                    LastNumber = random1.Next(1, 11);
                }

                else if(Level == enLevel.Medium)
                {
                    FirstNumber = random1.Next(11, 51);
                    LastNumber = random1.Next(11, 51);
                }

                else
                {
                    FirstNumber = random1.Next(51, 101);
                    LastNumber = random1.Next(51, 101);
                }
            }

            char GetOperationSymbol(enOperation operation)
            {
                char[] Operations = { '+', '-', '*', '/' };
                if(operation == enOperation.Mix)
                {
                    Random random = new Random();
                    return Operations[random.Next(0, 4)];
                }
                return Operations[(byte)operation - 1];
            }

            void SetCorrectAnswer()
            {
                if(Operation == enOperation.Add)
                {
                    CorrectAnswer = FirstNumber + LastNumber;
                }

                else if(Operation == enOperation.Sub)
                {
                    CorrectAnswer = FirstNumber - LastNumber;
                }

                else if (Operation == enOperation.Mult)
                {
                    CorrectAnswer = FirstNumber * LastNumber;
                }

                else if(Operation == enOperation.Div)
                {
                    CorrectAnswer = FirstNumber / LastNumber;
                }
            }

            void CheckAnswer()
            {
                if(PlayerAnswer == CorrectAnswer)
                {
                    IsCorrect = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Correct Answer :-)\n");
                }

                else
                {
                    IsCorrect = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong Answer :-)");
                    Console.WriteLine($"Correct Answer is {CorrectAnswer}");
                    Console.Write("\a");
                }
                Console.ResetColor();
            }

            public void PrintQuestion()
            {
                Console.WriteLine($"\t{FirstNumber}");
                Console.WriteLine("{0}", GetOperationSymbol(Operation));
                Console.WriteLine($"\t{LastNumber}");
                SetCorrectAnswer();
                Console.WriteLine("---------");
                ReadPlayerAnswer();
                CheckAnswer();
            }
        }

        string GetFinalResult(enResult Result)
        {
            string[] Results = { "Success", "Failure" };
            return Results[(byte)Result - 1];
        }

        string GetOperationName(enOperation operation)
        {
            string[] operations = { "Add", "Sub", "Mult", "Div", "Mix" };
            return operations[(byte)operation - 1];
        }

        string GetLevelName(enLevel level)
        {
            string[] levels = { "Easy", "Medium", "Hard", "Mix" };
            return levels[(byte)level - 1];
        }

        void PlayGame()
        {
            ReadQuestionsNumber();
            ReadLevel();
            ReadOperation();
            for(byte i = 1; i <= NumberOfQuestions; i++)
            {
                Console.WriteLine($"\nQuestion [{i}/{NumberOfQuestions}] : ");
                clsGameRound GameRound = new clsGameRound(GameLevel, Operation);
                GameRound.PrintQuestion();

                if(GameRound.IsCorrect)
                {
                    CorrectAnswersNumber++;
                }

                else
                {
                    WrongAnswersNumber++;
                }


            }

            if (CorrectAnswersNumber >= WrongAnswersNumber)
                 FinalResult =enResult.Success;
            else 
                FinalResult =enResult.Failure;
        }

        void PrintFinalResult()
        {
            Console.WriteLine("\t------------------------------------");
            Console.WriteLine($"\t\tFinal Result is {GetFinalResult(FinalResult)}");
            Console.WriteLine("\t------------------------------------");
            Console.WriteLine($"\t\tNumber of Questions    : {NumberOfQuestions}");
            Console.WriteLine($"\t\tGame Level             : {GetLevelName(GameLevel)}");
            Console.WriteLine($"\t\tGame Operation         : {GetOperationName(Operation)}");
            Console.WriteLine($"\t\tCorrect Answers Number : {CorrectAnswersNumber}");
            Console.WriteLine($"\t\tWrong Answers Number   : {WrongAnswersNumber}");
            
        }

        void ClearGame()
        {
            NumberOfQuestions = 0;
            CorrectAnswersNumber = 0;
            WrongAnswersNumber = 0;
        }

        public void StartGame()
        {
            char PlayAgain = 'Y';
            while (PlayAgain == 'Y' || PlayAgain == 'y')
            {
                Console.Clear();
                ClearGame();
                PlayGame();
                PrintFinalResult();
                Console.Write("\nDo You want to play again ?? ");
                PlayAgain = char.Parse(Console.ReadLine());
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            clsMathGame MathGame = new clsMathGame();
            MathGame.StartGame();
        }
    }
}
