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
            if (firstNode == null)
            {
                firstNode = lastNode = newNode;
                newNode.Next = newNode.Previous = null;
            }
            else
            {
                newNode.Next = firstNode;
                firstNode.Previous = newNode;
                firstNode = newNode;
            }
        }

        public void insertEnd(Node newNode)
        {
            if (lastNode == null)
            {
                firstNode = lastNode = newNode;
            }
            else
            {
                lastNode.Next = newNode;
                newNode.Previous = lastNode;
                lastNode = newNode;
            }
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
            if (node.Previous != null)
            {
                node.Previous.Next = node.Next;
            }
            else
            {
                firstNode = node.Next;
            }
            if (node.Next != null)
            {
                node.Next.Previous = node.Previous;
            }
            else
            {
                lastNode = node.Previous;
            }
        }

        public void swapNodes(Node node1, Node node2)
        {
            if (node1.Previous == node2 || node1.Next == node2)
            {
                Node nodeA, nodeB;
                if (node1.Previous == node2)
                {
                    nodeA = node2;
                    nodeB = node1;
                }
                else
                {
                    nodeA = node1;
                    nodeB = node2;
                }

                if (nodeA.Previous != null)
                {
                    nodeA.Previous.Next = nodeB;
                }
                else { firstNode = nodeB; }
                if (nodeB.Next != null)
                {
                    nodeB.Next.Previous = nodeA;
                }
                else { lastNode = nodeA; }

                Node nan = nodeA.Next;
                Node nap = nodeA.Previous;
                nodeA.Previous = nodeB;
                nodeA.Next = nodeB.Next;
                nodeB.Previous = nap;
                nodeB.Next = nodeA;
            } // if the nodes are adjacent
            else
            {
                Node n1p = node1.Previous;
                Node n1n = node1.Next;

                node1.Previous = node2.Previous;
                node1.Next = node2.Next;
                if (node1.Previous == null)
                {
                    node1.Next.Previous = node1;
                    firstNode = node1;
                }
                else if (node1.Next == null)
                {
                    node1.Previous.Next = node1;
                    lastNode = node1;
                }
                else
                {
                    node1.Previous.Next = node1;
                    node1.Next.Previous = node1;
                }

                node2.Previous = n1p;
                node2.Next = n1n;
                if (node2.Previous == null)
                {
                    node2.Next.Previous = node2;
                    firstNode = node2;
                }
                else if (node2.Next == null)
                {
                    node2.Previous.Next = node2;
                    lastNode = node2;
                }
                else
                {
                    node2.Previous.Next = node2;
                    node2.Next.Previous = node2;
                }
            }

        }

        public void printList()
        {
            if (firstNode != null)
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
        }

        public void appendLists(DoubleLinkedList list)
        {
            lastNode.Next = list.firstNode;
            lastNode = list.lastNode;
        }

        public void insertionSort()
        {
            for (int index = 1; index <= listLength(); index++)
            {
                Node current = firstNode;
                int i = index;
                while (i > 1) //select the next node
                {
                    i--;
                    current = current.Next;
                }

                while (current.Previous != null && current.Previous.Data > current.Data)
                {
                    swapNodes(current, current.Previous);
                }
            }
        }

        
    }

    class Program
    {
        static void Main()
        {
            TestLinkedList();


        }

        static DoubleLinkedList quickSort(DoubleLinkedList dll, Random rand)
        {
            Console.Write("going to sort: ");
            dll.printList();
            if (dll.listLength() > 2)
            {
                DoubleLinkedList dll2 = new DoubleLinkedList();
                Node partition = dll.firstNode;
                for (int i = 1; i < rand.Next(dll.listLength()); i++)
                {
                    partition = partition.Next;
                } // choose a random node

                dll.removeNode(partition);
                Console.WriteLine("My partition is " + partition.Data);

                Node node = dll.firstNode;
                while (node != null)
                {
                    if (node.Data < partition.Data)
                    {
                        dll2.insertBeginning(new Node(null, null, node.Data)); //Breaks everything?
                        if (node.Next != null)
                        {
                            node = node.Next;
                            dll.removeNode(node.Previous);
                        }
                        else
                        {
                            dll.removeNode(node);
                            node = null;
                        }
                    }
                    else
                    {
                        node = node.Next;
                    }
                }

                dll2 = quickSort(dll2, rand);
                dll = quickSort(dll, rand);
                dll2.insertEnd(partition);
                dll2.appendLists(dll);



                return dll2;
            }
            else
            {
                if (dll.firstNode != null)
                {
                    if(dll.firstNode.Next != null)
                    {
                        if (dll.firstNode.Data > dll.lastNode.Data)
                        {
                            dll.swapNodes(dll.firstNode, dll.lastNode);
                        }
                        return dll;
                    }
                    else
                    {
                        dll.lastNode = dll.firstNode;
                        return dll;
                    }
                }
                return dll;
            }
        }
        static void TestLinkedList()
        {
            DoubleLinkedList list = new DoubleLinkedList();
            Node node;
            list.insertBeginning(node = new Node(null, null, 1));
            list.insertBeginning(node = new Node(null, null, 0));
            list.lastNode = list.firstNode.Next;
            /*
            list.printList();

            Console.WriteLine("insert begining");
            list.insertBeginning(node = new Node(null, null, 2));
            list.printList();

            Console.WriteLine("insert end");
            list.insertEnd(node = new Node(null, null, 3));
            list.printList();

            Console.WriteLine("insert after first node");
            list.insertAfter(list.firstNode, node = new Node(null, null, 4));
            list.printList();

            Console.WriteLine("list length");
            Console.WriteLine(list.listLength());

            Console.WriteLine("find node (should be true)");
            Console.WriteLine(list.findNode(list.lastNode));

            Console.WriteLine("find node (should be false)");
            Console.WriteLine(list.findNode(node = new Node(null, null, 5)));

            Console.WriteLine("remove begining");
            list.removeBeginning();
            list.printList();

            Console.WriteLine("Remove 2nd node");
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
            list.swapNodes(list.firstNode.Next, list.lastNode);
            list.printList();

            Console.WriteLine("Insertion sort");
            list.insertionSort();
            list.printList(); */

            Console.WriteLine("Add more to sort");
            list.insertEnd(new Node(null, null, 5));
            list.insertBeginning(new Node(null, null, 3));
            list.insertEnd(new Node(null, null, 7));
            list.insertEnd(new Node(null, null, 4));
            list.printList();

            Console.WriteLine("Quicksort");
            list = quickSort(list, new Random());
            list.printList();

            Console.ReadLine();
        }
    }
}
