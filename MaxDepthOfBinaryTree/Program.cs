using System;

public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int value = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = value;
        this.left = left;
        this.right = right;
    }
}

public class Solution
{
    public int MaxDepth(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }

        int leftDepth = MaxDepth(root.left);
        int rightDepth = MaxDepth(root.right);

        return Math.Max(leftDepth, rightDepth) + 1;
    }
}

class Program
{
    static void Main()
    {
        // Пример 1
        TreeNode root1 = new TreeNode(3);
        root1.left = new TreeNode(9);
        root1.right = new TreeNode(20);
        root1.right.left = new TreeNode(15);
        root1.right.right = new TreeNode(7);

        Solution solution = new Solution();
        Console.WriteLine("Output for Example 1: " + solution.MaxDepth(root1)); // Output: 3

        // Пример 2
        TreeNode root2 = new TreeNode(1);
        root2.right = new TreeNode(2);

        Console.WriteLine("Output for Example 2: " + solution.MaxDepth(root2)); // Output: 2
    }
}
