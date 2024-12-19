using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            int width = 40;
            int height = 20;
            int score = 0;

            // Snake initialization
            Queue<(int x, int y)> snake = new Queue<(int x, int y)>();
            snake.Enqueue((10, 10));
            snake.Enqueue((9, 10));
            snake.Enqueue((8, 10));
            (int x, int y) snakeHead = (10, 10);

            // Food initialization
            Random rand = new Random();
            (int x, int y) food = (rand.Next(1, width - 1), rand.Next(1, height - 1));

            ConsoleKey direction = ConsoleKey.RightArrow;

            while (true)
            {
                // Clear screen
                Console.Clear();

                // Draw borders
                for (int i = 0; i < width; i++)
                    Console.Write("#");
                Console.WriteLine();
                for (int i = 0; i < height - 2; i++)
                    Console.WriteLine("#" + new string(' ', width - 2) + "#");
                for (int i = 0; i < width; i++)
                    Console.Write("#");
                Console.WriteLine();

                // Draw food
                Console.SetCursorPosition(food.x, food.y);
                Console.Write("*");

                // Draw snake
                foreach (var segment in snake)
                {
                    Console.SetCursorPosition(segment.x, segment.y);
                    Console.Write("O");
                }

                // Display score
                Console.SetCursorPosition(0, height);
                Console.WriteLine($"Score: {score}");

                // Input handling
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.UpArrow && direction != ConsoleKey.DownArrow) direction = key;
                    if (key == ConsoleKey.DownArrow && direction != ConsoleKey.UpArrow) direction = key;
                    if (key == ConsoleKey.LeftArrow && direction != ConsoleKey.RightArrow) direction = key;
                    if (key == ConsoleKey.RightArrow && direction != ConsoleKey.LeftArrow) direction = key;
                }

                // Calculate new head position
                switch (direction)
                {
                    case ConsoleKey.UpArrow: snakeHead = (snakeHead.x, snakeHead.y - 1); break;
                    case ConsoleKey.DownArrow: snakeHead = (snakeHead.x, snakeHead.y + 1); break;
                    case ConsoleKey.LeftArrow: snakeHead = (snakeHead.x - 1, snakeHead.y); break;
                    case ConsoleKey.RightArrow: snakeHead = (snakeHead.x + 1, snakeHead.y); break;
                }

                // Collision detection
                if (snakeHead.x <= 0 || snakeHead.x >= width - 1 || snakeHead.y <= 0 || snakeHead.y >= height - 1 || snake.Contains(snakeHead))
                {
                    Console.Clear();
                    Console.WriteLine("Game Over!");
                    Console.WriteLine($"Final Score: {score}");
                    break;
                }

                // Check if food is eaten
                if (snakeHead == food)
                {
                    score++;
                    food = (rand.Next(1, width - 1), rand.Next(1, height - 1));
                }
                else
                {
                    // Remove tail if food is not eaten
                    snake.Dequeue();
                }

                // Add new head position
                snake.Enqueue(snakeHead);

                // Game speed
                Thread.Sleep(100);
            }
        }
    }
}
