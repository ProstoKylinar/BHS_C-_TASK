using System;
using System.Collections.Generic;

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

public class BSTIterator
{
    private Stack<TreeNode> stack;

    public BSTIterator(TreeNode root)
    {
        stack = new Stack<TreeNode>();
        PushLeft(root);
    }

    // добавление всех левых узлов в стек
    private void PushLeft(TreeNode node)
    {
        while (node != null)
        {
            stack.Push(node);
            node = node.left;
        }
    }

    //возвращает true, если есть следующий элемент
    public bool HasNext()
    {
        return stack.Count > 0;
    }

    //возвращает следующий элемент
    public int Next()
    {
        TreeNode node = stack.Pop();
        int result = node.val;

        if (node.right != null)
        {
            PushLeft(node.right);
        }

        return result;
    }
}

class Program
{
    static void Main()
    {
        // создаем дерево по примеру
        TreeNode root = new TreeNode(7);
        root.left = new TreeNode(3);
        root.right = new TreeNode(15);
        root.right.left = new TreeNode(9);
        root.right.right = new TreeNode(20);
        BSTIterator bSTIterator = new BSTIterator(root);

        // тестирование
        Console.WriteLine(bSTIterator.Next());    // return 3
        Console.WriteLine(bSTIterator.Next());    // return 7
        Console.WriteLine(bSTIterator.HasNext()); // return True
        Console.WriteLine(bSTIterator.Next());    // return 9
        Console.WriteLine(bSTIterator.HasNext()); // return True
        Console.WriteLine(bSTIterator.Next());    // return 15
        Console.WriteLine(bSTIterator.HasNext()); // return True
        Console.WriteLine(bSTIterator.Next());    // return 20
        Console.WriteLine(bSTIterator.HasNext()); // return False
    }
}
