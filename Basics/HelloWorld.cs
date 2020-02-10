using System;
using System.Collections.Generic;
using System.Threading;

namespace Basics
{
    class HelloWorld
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            #region reference types
            Console.WriteLine("======= Reference types ========");
            Apple redApple = new Apple(10, Color.Red);
            Apple theSameRedApple = redApple;
            Console.WriteLine($"Weight of the red apple: {redApple.Weight}");
            Console.WriteLine($"Weight of the same red apple: {theSameRedApple.Weight}");
            redApple.Weight = 15;
            Console.WriteLine($"Weight of the red apple after change: {redApple.Weight}");
            Console.WriteLine($"Weight of the same red apple after change: {redApple.Weight}");

            #endregion
            Console.WriteLine();
            #region value types
            Console.WriteLine("======= Value types ========");
            int x = 10;
            int y = x;
            Console.WriteLine($"Value of x: {x}");
            Console.WriteLine($"Value of y: {y}");
            x++;
            Console.WriteLine($"Value of x after change: {x}");
            Console.WriteLine($"Value of y after change: {y}");
            #endregion
            Console.WriteLine();
            #region Use of TotalWeight static method
            Console.WriteLine("======== Usage of static TotalWeight method =========");
            Apple greenApple = new Apple(20, Color.Green);
            List<Apple> apples = new List<Apple>();
            apples.Add(redApple);
            apples.Add(greenApple);
            Console.WriteLine($"Weight of red apple: { redApple.Weight }");
            Console.WriteLine($"Weight of green apple: { greenApple.Weight }");
            Console.WriteLine($"Total weight of apples: {Apple.TotalWeight(apples)}");
            #endregion
            Console.WriteLine();
            #region Use of out parameter modifiers
            Console.WriteLine("======== Use of out modifier =========");
            Apple clonedGreenApple;
            Apple.CloneApple(greenApple, out clonedGreenApple);
            clonedGreenApple.Color = Color.Red;
            Console.WriteLine($"Change the color of cloned apple to red: { clonedGreenApple.Color }");
            Console.WriteLine($"Color of the original apple remains the same: { greenApple.Color }");
            #endregion
            Console.WriteLine();
            #region Use of ref parameter modifier
            Console.WriteLine("========= Use of ref modifier =========");
            Apple.GetNewGreenApple(ref redApple);
            Console.WriteLine($"The color of new green apple: { redApple.Color }");
            #endregion
            Console.WriteLine();
            #region Use of boxing and unboxing
            AppleWithPrice appleWithIntPrice = new AppleWithPrice(20, Color.Green, 30);
            AppleWithPrice appleWithFloatPrice = new AppleWithPrice(10, Color.Red, 20.5f);
            int intPrice = (int)appleWithIntPrice.Price;
            float floatPrice = (float)appleWithFloatPrice.Price;
            #endregion
            #region Use of static constructor
            Console.WriteLine("====== Use of static constructor ========");
            StaticConstructorExample staticConstructorExample = new StaticConstructorExample();
            StaticConstructorExample staticConstructorExample1 = new StaticConstructorExample();
            #endregion
            #region Threads
            Console.WriteLine("========= Use of threads ==========");
            Thread newThread = new Thread(new ThreadStart(RunMeInNewThread));      
            newThread.Start();
            for (int i = 0; i < 5; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*");
                Console.WriteLine(" *");
                Console.WriteLine("  *");
                Console.WriteLine("   *");
                Console.WriteLine("    *");
                Thread.Sleep(400);
            }
            
            #endregion

        }

        static void RunMeInNewThread()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(75);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("    *");
                Console.WriteLine("   *");
                Console.WriteLine("  *");
                Console.WriteLine(" *");
                Console.WriteLine("*");
                Thread.Sleep(400);
            }
            
        }
    }



    class Apple
    {
        public static int AppleCounter { get; private set; } = 0;
        private int weight;
        public int Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Weight must be a positive integer");
                }
                else
                {
                    weight = value;
                }
            }
        }
        public Color Color { get; set; }
        public Apple(int weight, Color color)
        {
            Weight = weight;
            Color = color;
            AppleCounter++;
        }
        public static int TotalWeight(List<Apple> apples)
        {
            int totalWeight = 0;
            foreach (var apple in apples)
            {
                totalWeight += apple.weight;
            }
            return totalWeight;
        }
        public static void CloneApple(Apple originalApple, out Apple clonedApple)
        {
            clonedApple = new Apple(originalApple.weight, originalApple.Color);
        }
        public static void GetNewGreenApple(ref Apple apple)
        {
            apple = new Apple(apple.weight, Color.Green);
        }
    }

    class AppleWithPrice : Apple
    {
        public Object Price { get; set; }
        public AppleWithPrice(int weight, Color color, Object price) : base(weight, color)
        {
            Price = price;
        }
    }

    enum Color
    {
        Red,
        Green
    }

    class StaticConstructorExample
    {
        static string firstPart = "http://www.example.com/";
        static string fullUrl;
        static string urlFragment = "foo/bar";

        static StaticConstructorExample()
        {
            fullUrl = firstPart + urlFragment;
            Console.WriteLine("I am the static constructor and I am called only once");
            Console.WriteLine(fullUrl);
        }

        public int CountWord(string word) { return 10; }
        public void OpenPage() 
        {
            // open full url
        }
    }

    class Integral
    {
        public delegate float F(float x);
        private F f;
        public float LeftBorder { get; set; }
        public float RightBorder { get; set; }
        public int N { get; set; }
        public Integral(F del, float leftBorder, float rightBorder, int n)
        {
            f = del;
            LeftBorder = leftBorder;
            RightBorder = rightBorder;
            N = n;
        }
        public float CalculateIntegral()
        {
            float step = (RightBorder - LeftBorder) / N;
            float result = 0;
            float threadResult = 0;
            for (int i = 0; i < N; i++)
            {
                result += f(LeftBorder + step * (i + 0.5f));
            }
            result *= step;
            return result;
        }
    }
}
