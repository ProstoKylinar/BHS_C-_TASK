using System;

public class Solution
{
    public int NumIslands(char[][] grid)
    {
        if (grid == null || grid.Length == 0)
        {
            return 0;
        }

        int numIslands = 0;

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == '1')
                {
                    numIslands++;
                    DFS(grid, i, j);
                }
            }
        }

        return numIslands;
    }

    private void DFS(char[][] grid, int i, int j)
    {
        // проверка границ и воды
        if (i < 0 || i >= grid.Length || j < 0 || j >= grid[i].Length || grid[i][j] == '0')
        {
            return;
        }

        // пометить текущую ячейку как посещённую
        grid[i][j] = '0';

        // рекурсивный вызов для всех соседних ячеек
        DFS(grid, i + 1, j); // вниз
        DFS(grid, i - 1, j); // вверх
        DFS(grid, i, j + 1); // вправо
        DFS(grid, i, j - 1); // влево
    }
}

class Program
{
    static void Main()
    {
        var solution = new Solution();

        char[][] grid1 = {
            new char[] { '1', '1', '1', '1', '0' },
            new char[] { '1', '1', '0', '1', '0' },
            new char[] { '1', '1', '0', '0', '0' },
            new char[] { '0', '0', '0', '0', '0' }
        };
        Console.WriteLine("Output for Example 1: " + solution.NumIslands(grid1)); // Output: 1

        char[][] grid2 = {
            new char[] { '1', '1', '0', '0', '0' },
            new char[] { '1', '1', '0', '0', '0' },
            new char[] { '0', '0', '1', '0', '0' },
            new char[] { '0', '0', '0', '1', '1' }
        };
        Console.WriteLine("Output for Example 2: " + solution.NumIslands(grid2)); // Output: 3
    }
}
