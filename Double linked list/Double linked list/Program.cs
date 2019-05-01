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
        public int data;
        public Node next;
    }

    // this holds the head of the list
    class List
    {
        public Node firstNode;
        public Node lastNode;
    }
    class Program
    {
        static void insertBeginning(List list, Node newNode)
        {
            newNode.next = list.firstNode;
            list.firstNode = newNode;
        }

        static Node removeBeginning(List list)
        {
            Node returnNode;
            returnNode = list.firstNode;
            list.firstNode = list.firstNode.next;
            return (returnNode);
        }

        static void insertAfter(Node node, Node newNode)
        {
            newNode.next = node.next;
            node.next = newNode;
        }

        static Node removeAfter(Node node)
        {
            Node returnNode;
            returnNode = node.next;
            node.next = node.next.next;
            return (returnNode);
        }

        static void printList(List list)
        {
            Node node = list.firstNode;
            while (node != null)
            {
                System.Console.Write(node.data);
                System.Console.Write(" -> ");
                node = node.next;
            }
            System.Console.WriteLine("");
        }

        static void Main()
        {
            List list = new List(); ;
            Node node, node2;
            int i;

            // add a few nodes 
            for (i = 0; i < 4; i = i + 1)
            {
                node = new Node();
                node.data = i;
                insertBeginning(list, node);
            }
            printList(list); // now we have nodes

            node2 = new Node();
            node2.data = 6;
            insertAfter(list.firstNode, node2);
            printList(list);

            removeBeginning(list); // Note: the returned Node is silently discarded
            printList(list);

            removeAfter(list.firstNode);  // ... here as well ...
            printList(list);

            for (i = 1; i < 4; i = i + 1)
                removeBeginning(list);

            printList(list);

            removeBeginning(list);  // if we remove too many Nodes we get an error
            printList(list); // our functions have no error checking 

        }
    }

}


