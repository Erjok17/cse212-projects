using System;
using System.Collections.Generic;

public static class Recursion
{
    /// Problem 1: Sum of squares using recursion
    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0)
            return 0;
        return n * n + SumSquaresRecursive(n - 1);
    }

    /// Problem 2: Permutations of given size from letters
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        for (int i = 0; i < letters.Length; i++)
        {
            char current = letters[i];
            string remaining = letters.Remove(i, 1);
            PermutationsChoose(results, remaining, size, word + current);
        }
    }

    /// Problem 3: Count ways to climb stairs (1, 2, or 3 steps at a time)
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        if (remember == null)
            remember = new Dictionary<int, decimal>();

        // Base Cases
        if (s == 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // Check if we already solved this
        if (remember.ContainsKey(s))
            return remember[s];

        // Solve using recursion with memoization
        decimal ways = CountWaysToClimb(s - 1, remember) 
                     + CountWaysToClimb(s - 2, remember) 
                     + CountWaysToClimb(s - 3, remember);
        
        remember[s] = ways;
        return ways;
    }

    /// Problem 4: Generate all binary strings from wildcard pattern
    public static void WildcardBinary(string pattern, List<string> results)
    {
        int wildcardIndex = pattern.IndexOf('*');
        if (wildcardIndex == -1)
        {
            results.Add(pattern);
            return;
        }

        // Replace first * with 0 and 1
        string withZero = pattern.Substring(0, wildcardIndex) + "0" + pattern.Substring(wildcardIndex + 1);
        string withOne = pattern.Substring(0, wildcardIndex) + "1" + pattern.Substring(wildcardIndex + 1);

        WildcardBinary(withZero, results);
        WildcardBinary(withOne, results);
    }

    /// Problem 5: Solve maze using backtracking
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // Add current position to path
        currPath.Add((x, y));

        // Check if we reached the end
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }

        // Try all possible directions
        foreach (var (dx, dy) in new[] { (1, 0), (0, 1), (-1, 0), (0, -1) })
        {
            int newX = x + dx;
            int newY = y + dy;
            
            if (maze.IsValidMove(currPath, newX, newY))
            {
                SolveMaze(results, maze, newX, newY, currPath);
            }
        }

        // Backtrack
        currPath.RemoveAt(currPath.Count - 1);
    }
}