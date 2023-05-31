
using System;
using System.Collections.Generic;

namespace TurnBasedFighter
{
    class Player
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int DamageMin { get; set; }
        public int DamageMax { get; set; }

        public void Attack(Player otherPlayer)
        {
            Random random = new Random();

            int damage = random.Next(DamageMin, DamageMax);

            Console.WriteLine("{0} attacks {1} with {2} damage!", Name, otherPlayer.Name, damage);

            otherPlayer.Health -= damage;

            Console.WriteLine("{0} now has {1} health remaining!", otherPlayer.Name, otherPlayer.Health);
        }

        public bool IsDead()
        {
            return Health <= 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Player player1 = new Player
            {
                Name = "Player 1",
                Health = 100,
                Level = 1,
                Experience = 0,
                DamageMin = 5,
                DamageMax = 10
            };

            Player player2 = new Player
            {
                Name = "Player 2",
                Health = 100,
                Level = 1,
                Experience = 0,
                DamageMin = 5,
                DamageMax = 10
            };

            List<Player> players = new List<Player>();
            players.Add(player1);
            players.Add(player2);

            int currentPlayerIndex = 0;

            while (true)
            {
                Console.WriteLine("{0}, it's your turn!", players[currentPlayerIndex].Name);

                bool didAttack = false;

                while (!didAttack)
                {
                    Console.WriteLine("Who do you want to attack?");

                    for (int i = 0; i < players.Count; i++)
                    {
                        if (i == currentPlayerIndex) continue;

                        Console.WriteLine("{0}. {1} - Health: {2}", i + 1, players[i].Name, players[i].Health);
                    }

                    try
                    {
                        int selection = Convert.ToInt32(Console.ReadLine()) - 1;

                        if (selection < 0 || selection >= players.Count) throw new Exception();

                        players[currentPlayerIndex].Attack(players[selection]);

                        didAttack = true;
                    }
                    catch
                    {
                        Console.WriteLine("Invalid selection, try again!");
                    }
                }

                if (players[currentPlayerIndex].IsDead())
                {
                    Console.WriteLine("{0} has been defeated!", players[currentPlayerIndex].Name);
                    break;
                }

                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            }

            Console.WriteLine("Game over!");
            Console.ReadLine();
        }
    }
}