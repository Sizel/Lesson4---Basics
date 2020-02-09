using System;
using System.Collections.Generic;

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
            #region Use of boxing
            
            #endregion

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

    enum Color
    {
        Red,
        Green
    }
}
