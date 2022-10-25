using System;
using System.Windows.Controls;

namespace GoodsForPets.Helpers
{
    public static class Helper
    {
        public static Button LogoutButton { get; set; }
        public static Label UserFullName { get; set; }
        public static int AuthorizedUserRole { get; set; }

        public static int SearchAlgorithm(string firstString, string secondString)
        {
            if (firstString == null) throw new ArgumentNullException(nameof(firstString));
            if (secondString == null) throw new ArgumentNullException(nameof(secondString));

            int difference;

            int[,] map = new int[firstString.Length + 1, secondString.Length + 1];

            for (int i = 0; i < firstString.Length; i++) map[i, 0] = i;
            for (int j = 0; j < secondString.Length; j++) map[0, j] = j;

            for (int i = 1; i <= firstString.Length; i++)
            {
                for (int j = 1; j <= secondString.Length; j++)
                {
                    difference = (firstString[i - 1] == secondString[j - 1]) ? 0 : 1;
                    map[i, j] = Math.Min(Math.Min(map[i - 1, j] + 1, map[i, j - 1] + 1), map[i - 1, j - 1] + difference);
                }
            }

            return map[firstString.Length, secondString.Length];
        }
    }
}
