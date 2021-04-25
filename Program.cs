using System;

namespace SanPedro_Exer2
/*
Name: Maria Lourdes S. San Pedro
Student Number: 2019-30100
Date:
Brief description: This program is a linked list implementation of a binary search tree.
*/

{
    //Create a public Node class and its attributes
    public class Node
    {
        public int? Data { get; set; } //Node's value
        public Node Parent { get; set; } //Node's parent in the BST
        public Node Left { get; set; } //Node's left to help us traverse the BST
        public Node Right { get; set; } //Node's right to help us traverse the BST

        //Create a class constructor for the Node class with an int parameter
        public Node(int input)
        {
            Data = input;
        }
    }

    //Create a public BST class
    public class BST
    {
        //Create a Root variable of Node class
        public Node Root { get; set; }


        //Method to insert node
        public void InsertNode(int input)
        {
            Node newNode = new Node(input); //Create a new node object
            if (Root == null) //Check if Root of BST is null
            {
                Root = newNode;
            }

            else
            {
                Node nodePointer = Root; //Pointer of current node which starts at the root
                Node parentPointer = nodePointer.Parent; //Pointer of parent to which our new node will be attached
                while (true)
                {
                    parentPointer = nodePointer; //Next parent is the current node
                    if (newNode.Data < nodePointer.Data) //If new node is less than current node
                    {
                        nodePointer = nodePointer.Left; //Go left - Left node becomes current node
                        if (nodePointer == null) //If no left node exists, node is inserted to BST by attaching it to the parent
                        {
                            parentPointer.Left = newNode;
                            newNode.Parent = parentPointer;
                            break;
                        }
                    }
                    else if (newNode.Data > nodePointer.Data) //If new node is greater than current node
                    {
                        nodePointer = nodePointer.Right; //Go right - Right node becomes current node
                        if (nodePointer == null) //If no right node exists, node is inserted to BST by attaching it to parent
                        {
                            parentPointer.Right = newNode;
                            newNode.Parent = parentPointer;
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Value already exists. The BST does not accept duplicate values."); 
                        break;
                    }
                }

            }



        }

        //Method to delete node
        public void DeleteNode(int input)
        {
            Node nodePointer = Root; //Pointer of current node which starts at the root
            Node parentPointer = nodePointer.Parent; //Pointer of parent to which our new node will be attached

            if (nodePointer == null) //BST is empty
            {
                Console.WriteLine("There are no nodes to delete.");
            }
            else
            {
                while (nodePointer != null)
                {
                    if (input == nodePointer.Data) //If node to be deleted is equal to root/current node
                    {
                        break;
                    }
                    parentPointer = nodePointer; //Next parent is the current node
                    nodePointer = input < nodePointer.Data ? nodePointer.Left : nodePointer.Right; //If node to be deleted is less than the current node, go left; else go right; nodePointer value is updated to next node
                }
                if (nodePointer == null) //Node to be deleted is not in the BST
                {
                    Console.WriteLine(input + " is not an existing node!");
                }
                else
                {
                    if (nodePointer.Left != null && nodePointer.Right != null) //If current node has both left and right children
                    {
                        Node rightNode = nodePointer.Right; //Create Node object for right subtree  
                        parentPointer = nodePointer; //Next parent is the current node
                        while (rightNode.Left != null) //Successor is minimum of right subtree
                        {
                            parentPointer = rightNode; //Shift parent and successor pointers
                            rightNode = rightNode.Left; 
                        }
                        nodePointer.Data = rightNode.Data; //Swap data of successor and node to be deleted
                        nodePointer = rightNode;
                    }
                    //If node has 1 or 0 children
                    if (parentPointer == null) //If node to be deleted is the root
                    {
                        Root = nodePointer.Left != null ? nodePointer.Left : nodePointer.Right; //Replace the root with its left or rught child
                    }
                    else
                    {
                        Node childDelNode = nodePointer.Left != null ? nodePointer.Left : nodePointer.Right; //Create Node object for child of the deleted node
                        if (nodePointer == parentPointer.Left) //Link child to parent of deleted node
                        {
                            parentPointer.Left = childDelNode;
                        }
                        else
                        {
                            parentPointer.Right = childDelNode;
                        }
                    }
                    Console.WriteLine("Node " + input + " has been deleted.");
                }

            }
        }

        //Method to get the minimum value in BST
        public void getMin()
        {
            Node nodePointer = Root;
            if (nodePointer == null) //If BST is empty
            {
                Console.WriteLine("There are no nodes in the BST.");
            }
            else
            {
                while (nodePointer.Left != null) //Continue going left until we get leftmost node
                {
                    nodePointer = nodePointer.Left;
                }
                int? BSTMin = nodePointer.Data; 
                Console.WriteLine("The minimum value is " + BSTMin + ".");
            }
        }

        //Method to get the maximum value in BST
        public void getMax()
        {
            Node nodePointer = Root;
            if (nodePointer == null) //If BST is empty
            {
                Console.WriteLine("There are no nodes in the BST.");
            }
            else
            {
                while (nodePointer.Right != null) //Continue going right until we get rightmost node
                {
                    nodePointer = nodePointer.Right;
                }
                int? BSTMax = nodePointer.Data; 
                Console.WriteLine("The maximum value is " + BSTMax + ".");
            }
        }

        //Method to search for node in BST
        public Node SearchNode(int key)
        {
            Node nodePointer = Root;
            while (nodePointer != null)
            {
                if (nodePointer.Data == key) //If current node is equal to node to search for
                {
                    return nodePointer; //Return node 
                }
                if (nodePointer.Data == null) //If current node is equal to null
                {
                    return null; //Return null
                }
                //To traverse within the BST
                if (key > nodePointer.Data) //If node to search for is greater than current node
                {
                    nodePointer = nodePointer.Right; //Go right
                }
                else
                {
                    nodePointer = nodePointer.Left; //Else, go left
                }
            }
            return nodePointer; //Return new current node
        }



        //Method to get node's successor in BST
        public int? getSuccessor(int key)
        {
            Node nodePointer = SearchNode(key); //Current node is the key parameter if found in BST using the SearchNode method
            int? SuccessorValue = null; //Create nullable int variable to store successor value
            if (nodePointer.Right != null) //If the current node has a right subtree
            {
                Node SuccessorNode = nodePointer.Right; //Create Node object to store the right subtree of the current node
                while (SuccessorNode.Left != null) //Check if the right subtree has left child
                {
                    SuccessorNode = SuccessorNode.Left; //Successor node is the leftmost child of the right subtree
                }
                SuccessorValue = SuccessorNode.Data;
            }
            else //If the current node does not have a right subtree
            {
                while (nodePointer.Parent != null) //Current node parent is not null
                {
                    if (nodePointer.Parent.Left == nodePointer) //If the current node is equal to the parent's left child
                    {
                        SuccessorValue = nodePointer.Parent.Data; //Successor node is the parent of the current node
                        break;
                    }
                    else //The current node is not equal to the parent's left child
                    {
                        nodePointer = nodePointer.Parent; //Shift current node pointer to current node's parent
                    }
                }
            }
            return SuccessorValue;
        }

        //Method to get node's predecessor in BST
        public int? getPredecessor(int key)
        {
            Node nodePointer = SearchNode(key); //Current node is the key parameter if found in BST using the SearchNode method
            int? PredecessorValue = null; //Create nullable int variable to store successor value
            if (nodePointer.Left != null)  //If the current node has a left subtree
            {
                Node PredecessorNode = nodePointer.Left; //Create Node object to store the left subtree of the current node
                while (PredecessorNode.Right != null)  //Check if the left subtree has right child
                {
                    PredecessorNode = PredecessorNode.Right; //Predecessor node is the rightmost child of the left subtree
                }
                PredecessorValue = PredecessorNode.Data;             
            }
            else //The current node has a left subtree
            {
                while (nodePointer.Parent != null) //Current node parent is not null
                {
                    if (nodePointer.Parent.Right == nodePointer) //If the current node is equal to the parent's right child
                    {
                        PredecessorValue = nodePointer.Parent.Data; //Predecessor is the parent of the current node
                        break;
                    }
                    else //If current node is not equal to the parent's right child
                    {
                        nodePointer = nodePointer.Parent; //Shift current node pointer to current node's parent
                    }
                }
            }
            return PredecessorValue;
        }

        /*InOrderTraversal : The idea of inorder traversal is that we visit the nodes in the order left-root-right, meaning for any subtree in the path, left node must be visited first followed by root and right node.

        Left, Root, Right
        Prints the values in ascending order
            */

        //Inorder traversal
        public void Inorder(Node Root)
        {
            if (Root != null) //If BST is not empty
            {
                Inorder(Root.Left); //Traverse the left subtree
                {
                    Console.Write(Root.Data + " "); //Print the value of the node
                }
                Inorder(Root.Right); //Traverse the right subtree
            }
        }

    }




    class Program
    {
        public static void Main(string[] args)
        {
            BST newBST = new BST();//Create new BST called newBST
            Console.WriteLine("[1] Insert node to binary tree");
            Console.WriteLine("[2] Delete node from binary tree");
            Console.WriteLine("[3] Minimum");
            Console.WriteLine("[4] Maximum");
            Console.WriteLine("[5] Successor");
            Console.WriteLine("[6] Predecessor");
            Console.WriteLine("[7] Search");
            Console.WriteLine("[8] Print BST");
            Console.WriteLine("[9] Exit");
            while (true)
            {
                Console.Write("Enter a number depending on the operation you'd like to perform.");
                int userMenuInput = Convert.ToInt32(Console.ReadLine());//Get user input for what they would like to do
                int input;
                switch (userMenuInput)
                {
                    case 1:
                        Console.WriteLine("Enter the node you'd like to insert into the BST.");
                        input = Convert.ToInt32(Console.ReadLine());
                        newBST.InsertNode(input);
                        break;

                    case 2:
                        Console.WriteLine("Enter an existing node: ");
                        input = Convert.ToInt32(Console.ReadLine());
                        newBST.DeleteNode(input);
                        break;

                    case 3:
                        newBST.getMin();
                        break;

                    case 4:
                        newBST.getMax();
                        break;

                    case 5:
                        Console.WriteLine("Enter an existing node:");
                        input = Convert.ToInt32(Console.ReadLine());
                        if (newBST.SearchNode(input) != null) //To check if the input value is in the tree
                        {
                            int? Successor = newBST.getSuccessor(input);
                            if (Successor != null) //If the input has successor
                            {
                                Console.WriteLine("The successor of " + input + " is " + Successor + " .");
                            }
                            else
                            {
                                Console.WriteLine(input + " has no successor.");
                            }
                        }
                        else
                        {
                            Console.WriteLine(input + " is not an existing node!");
                        }
                        break;

                    case 6:
                        Console.WriteLine("Enter an existing node:");
                        input = Convert.ToInt32(Console.ReadLine());
                        if (newBST.SearchNode(input) != null) //To check if the input value is in the tree
                        {
                            int? Predecessor = newBST.getPredecessor(input);
                            if (Predecessor != null) //If the input has a predecessor
                            {
                                Console.WriteLine("The predecessor of " + input + " is " + Predecessor + " .");
                            }
                            else
                            {
                                Console.WriteLine(input + " has no predecessor.");
                            }
                        }
                        else
                        {
                            Console.WriteLine(input + " is not an existing node!");
                        }
                        break;

                    case 7:
                        Console.WriteLine("Enter the number to be searched: ");
                        input = Convert.ToInt32(Console.ReadLine());
                        if (newBST.SearchNode(input) != null) //To check if the input value is in the tree
                        {
                            Console.WriteLine("Node with value of " + input + " was found in the BST.");
                        }
                        else
                        {
                            Console.WriteLine("Node with value of " + input + " was not found in the BST.");
                        }
                        break;
                    case 8:
                        if (newBST.Root != null)
                        {
                            newBST.Inorder(newBST.Root);
                        }
                        else
                        {
                            Console.Write("Nothing to print! There are no nodes in the BST.");
                        }
                        Console.WriteLine();
                        break;
                    case 9: //exit
                        System.Environment.Exit(1);
                        break;
                }
            }
        }
    }
}


