using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Double_linked_list
{


    // single elements in the list 
    class Node 
    {
        public int Data { get; set; }
        public Node Next { get; set; }     // Next/Previous might want "private set" if
        public Node Previous { get; set; }

        public Node(Node next, Node prev, int data) //Can just pass through other nodes as it passes references, meaning there arent duplicates in memory
        {
            Next = next;
            Previous = prev;
            Data = data;
        }
    }

    // this holds the head of the list
    class DoubleLinkedList
    {
        public Node firstNode;
        public Node lastNode;

        public void insertBeginning(Node newNode)
        {
            newNode.Next = firstNode;
            firstNode = newNode;
        }

        public void insertEnd(Node newNode)
        {
            lastNode.Next = newNode;
            lastNode = newNode;
        }

        public void insertAfter(Node node, Node newNode)
        {
            node.Next.Previous = newNode;
            newNode.Next = node.Next;
            newNode.Previous = node.Previous;
            node.Next = newNode;
        }

        public int listLength()
        { 
            int i = 0;
            Node node = firstNode;
            while (node != null)
            {
                i++;
                node = node.Next;
            }
            return i;
        }

        public bool findNode(Node toFind)
        {
            Node node = firstNode;
            while (node != null)
            {
                if (node == toFind)
                {
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        public void removeBeginning()
        {
            firstNode = firstNode.Next;
            firstNode.Previous = null;
        }

        public void removeNode(Node node)
        {
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
        }

        public void swapNodes(Node node1, Node node2)
        {
            Console.WriteLine("");
            Console.WriteLine(node1.Previous.Data);
            Console.WriteLine(node1.Next.Data);
            Console.WriteLine(node2.Previous.Data);
            Console.WriteLine(node2.Next.Data);
            Console.WriteLine("");
            Node node1P = node1.Previous;
            Node node1N = node1.Next;
            node1.Previous = node2.Previous;
            node1.Next = node2.Next;
            node2.Previous = node1P;
            node2.Next = node1N;
        }

        public void printList()
        {
            Node node = firstNode;
            System.Console.Write(node.Data);
            node = node.Next;
            while (node != null)
            {
                System.Console.Write(" -> ");
                System.Console.Write(node.Data);
                node = node.Next;
            }
            System.Console.WriteLine("");
        }

        public void appendLists(DoubleLinkedList list)
        {
            lastNode.Next = list.firstNode;
            lastNode = list.lastNode;
        }
    }

    class Program
    {
        static void Main()
        {
            DoubleLinkedList list = new DoubleLinkedList();
            Node node;
            list.insertBeginning(node = new Node(null, null, 1));
            list.insertBeginning(node = new Node(null, null, 0));
            list.lastNode = list.firstNode.Next;
            list.printList();

            Console.WriteLine("in begining");
            list.insertBeginning(node = new Node(null, null, 2));
            list.printList();

            Console.WriteLine("in end");
            list.insertEnd(node = new Node(null, null, 3));
            list.printList();

            Console.WriteLine("in after first node");
            list.insertAfter(list.firstNode, node = new Node(null, null, 4));
            list.printList();

            Console.WriteLine("find node t");
            Console.WriteLine(list.findNode(list.lastNode));

            Console.WriteLine("find node f");
            Console.WriteLine(list.findNode(node = new Node(null, null, 5)));

            Console.WriteLine("remove begining");
            list.removeBeginning();
            list.printList();

            Console.WriteLine("Remove");
            list.removeNode(list.firstNode.Next);
            list.printList();

            Console.WriteLine("append");
            DoubleLinkedList dll = new DoubleLinkedList();
            dll.insertBeginning(new Node(null, null, 8));
            dll.insertBeginning(new Node(null, null, 9));
            dll.lastNode = dll.firstNode.Next;
            list.appendLists(dll);
            list.printList();

            Console.WriteLine("swap");
            list.swapNodes(list.firstNode, list.lastNode);
            list.printList();

            


            Console.ReadLine();


        }
    }
}
