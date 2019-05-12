using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Double_linked_list
{


    // single elements in the list 
    class Node 
    {
        public int Data { get; set; } //the data in the node
        public Node Next { get; set; }   //the next node in the list
        public Node Previous { get; set; }//the previous node in the list

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
        public Node firstNode; //the first node in the list
        public Node lastNode;//the last node in the list

    }

    class Program
    {
        static void Main()
        {
            runTimeTests();
        }

        static void insertBeginning(DoubleLinkedList l, Node newNode) //Insert a node into the begining of the list
        {
            if (l.firstNode == null) //if the list is empty
            {
                l.firstNode = l.lastNode = newNode; //make this node the first and last node
                newNode.Next = newNode.Previous = null;
            }
            else
            {
                newNode.Next = l.firstNode; //add the node to the begining
                l.firstNode.Previous = newNode;
                l.firstNode = newNode;
            }
        }

        static void insertEnd(DoubleLinkedList l, Node newNode)//Insert a node into the end of the list
        {
            if (l.lastNode == null) //if there isnt a node in the list
            {
                l.firstNode = l.lastNode = newNode; //make this the first and the last node
            }
            else
            {
                l.lastNode.Next = newNode; //add this node to the end
                newNode.Previous = l.lastNode;
                l.lastNode = newNode;
            }
        }

        static void insertAfter(DoubleLinkedList l, Node node, Node newNode)//Insert a node after a given node in the list
        {
            node.Next.Previous = newNode;
            newNode.Next = node.Next;
            newNode.Previous = node.Previous;
            node.Next = newNode;
        }

        static int listLength(DoubleLinkedList l)//get the length of the list
        {
            int i = 0;
            Node node = l.firstNode;
            while (node != null) //cycle through the list
            {
                i++; //count each time
                node = node.Next;

            }
            return i;
        }

        static bool findNode(DoubleLinkedList l, Node toFind) //find if a node is in the list
        { 
            Node node = l.firstNode;
            while (node != null) //while the node isnt found cycle through the nodes
            {
                if (node == toFind) //if the node matches the given node
                {
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        static void removeBeginning(DoubleLinkedList l) //remove the node at the end of the list
        {
            l.firstNode = l.firstNode.Next;//call the next node the first node
            l.firstNode.Previous = null; //remove the old node
        }

        static void removeNode(DoubleLinkedList l, Node node) //remove a given node
        {
            if (node.Previous != null)//if it has a previous node make it point to the next node
            {
                node.Previous.Next = node.Next; 
            }
            else
            {
                l.firstNode = node.Next;
            }
            if (node.Next != null) //if there is a next node make it point to the previous
            {
                node.Next.Previous = node.Previous;
            }
            else
            {
                l.lastNode = node.Previous;
            }
        }

        static void swapNodes(DoubleLinkedList l, Node node1, Node node2)// swap the positions of two nodes in the list
        {
            if (node1.Previous == node2 || node1.Next == node2) //if the nodes are next to each other
            {
                Node nodeA, nodeB;
                if (node1.Previous == node2) //rename them for consistency
                {
                    nodeA = node2;
                    nodeB = node1;
                }
                else
                {
                    nodeA = node1;
                    nodeB = node2;
                }

                if (nodeA.Previous != null) //swap all references
                {
                    nodeA.Previous.Next = nodeB;
                }
                else { l.firstNode = nodeB; }
                if (nodeB.Next != null)
                {
                    nodeB.Next.Previous = nodeA;
                }
                else { l.lastNode = nodeA; }

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

                node1.Previous = node2.Previous; //swap all references to them
                node1.Next = node2.Next;
                if (node1.Previous == null)
                {
                    node1.Next.Previous = node1;
                    l.firstNode = node1;
                }
                else if (node1.Next == null)
                {
                    node1.Previous.Next = node1;
                    l.lastNode = node1;
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
                    l.firstNode = node2;
                }
                else if (node2.Next == null)
                {
                    node2.Previous.Next = node2;
                    l.lastNode = node2;
                }
                else
                {
                    node2.Previous.Next = node2;
                    node2.Next.Previous = node2;
                }
            }

        }

        static void printList(DoubleLinkedList l) //display the list
        {
            if (l.firstNode != null) //if there are nodes in the list
            {
                Node node = l.firstNode;
                Console.Write(node.Data);
                node = node.Next;
                while (node != null) //cycle through the nodes
                {
                    Console.Write(" -> "); //put an arrow between them for easy reading
                    Console.Write(node.Data); //display the nodes data
                    node = node.Next;
                }
                System.Console.WriteLine(""); //end it with a line break
            }
        }

        static void appendLists(DoubleLinkedList l, DoubleLinkedList list) //add another list to a list
        {
            l.lastNode.Next = list.firstNode; //join them through references
            list.firstNode.Previous = l.lastNode;
            l.lastNode = list.lastNode; //edit the last node to reference the last node of the 2nd list
        }

        static void insertionSort(DoubleLinkedList l) //sort the list using insertion sort
        {
            for (int index = 1; index <= listLength(l); index++) //go trough each node
            {
                Node current = l.firstNode;
                int i = index;
                while (i > 1) //select the next node
                {
                    i--;
                    current = current.Next;
                }

                while (current.Previous != null && current.Previous.Data > current.Data) //swap untill its in the right place
                {
                    swapNodes(l, current, current.Previous);
                }
            }
        }

        static DoubleLinkedList quickSort(DoubleLinkedList l, Random rand)
        {
            Console.Write("going to sort: ");
            printList(l);
            if (listLength(l) > 2) //if the list needs sorting
            {
                DoubleLinkedList l2 = new DoubleLinkedList();
                Node partition = l.firstNode;
                for (int i = 1; i < rand.Next(listLength(l)); i++)// choose a random node
                {
                    partition = partition.Next;
                } 

                removeNode(l, partition); //remove the partition from the lists to be sorted

                Node node = l.firstNode;
                while (node != null) //go through all the nodes
                {
                    if (node.Data < partition.Data) //compare it to the partition
                    {
                        insertBeginning(l, new Node(null, null, node.Data)); //add it to the new small list
                        if (node.Next != null)
                        {
                            node = node.Next;
                            removeNode(l, node.Previous);
                        }
                        else
                        {
                            removeNode(l, node);
                            node = null;
                        }
                    }
                    else
                    {
                        node = node.Next; //leave it in the big list
                    }
                }

                l2 = quickSort(l2, rand); //sort the small list
                l = quickSort(l, rand); //sort the big list
                insertEnd(l2, partition); //put the partition back in the midle of the lists
                appendLists(l, l2);



                return l2;
            }
            else //if the list doesnt need sorting
            {
                if (l.firstNode != null) //if there are nodes in this list
                {
                    if(l.firstNode.Next != null)
                    {
                        if (l.firstNode.Data > l.lastNode.Data) //swap the nodes if they are in the wrong order
                        {
                            swapNodes(l, l.firstNode, l.lastNode);
                        }
                        return l;
                    }
                    else
                    {
                        l.lastNode = l.firstNode;
                        return l;
                    }
                }
                return l;
            }
        }

        static void runTimeTests()
        {
            Random random = new Random();
            Stopwatch sw = new Stopwatch();
            DoubleLinkedList list = new DoubleLinkedList();

            Console.Write("Insertion sort with 100 nodes takes ");
            for (int i = 0; i < 100; i++)
            {
                insertBeginning(list, new Node(null, null, random.Next(0, 10000)));
            }

            sw.Start();
            insertionSort(list);
            long time = sw.ElapsedMilliseconds;
            Console.WriteLine(time.ToString() + "ms");

            Console.Write("Insertion sort with 1000 nodes takes ");
            list = new DoubleLinkedList();
            for (int i = 0; i < 1000; i++)
            {
                insertBeginning(list, new Node(null, null, random.Next(0, 10000)));
            }

            sw.Reset();
            sw.Start();
            insertionSort(list);
            time = sw.ElapsedMilliseconds;
            Console.WriteLine(time.ToString() + "ms");

            Console.Write("Insertion sort with 10000 nodes takes ");
            list = new DoubleLinkedList();
            for (int i = 0; i < 10000; i++)
            {
                insertBeginning(list, new Node(null, null, random.Next(0, 10000)));
            }

            sw.Reset();
            sw.Start();
            insertionSort(list);
            time = sw.ElapsedMilliseconds;
            Console.WriteLine(time.ToString() + "ms");

            Console.Write("Insertion sort with 100000 nodes takes ");
            list = new DoubleLinkedList();
            for (int i = 0; i < 100000; i++)
            {
                insertBeginning(list, new Node(null, null, random.Next(0, 10000)));
            }

            sw.Reset();
            sw.Start();
            insertionSort(list);
            time = sw.ElapsedMilliseconds;
            Console.WriteLine(time.ToString() + "ms");

            Console.Write("Insertion sort with 1000000 nodes takes ");
            list = new DoubleLinkedList();
            for (int i = 0; i < 1000000; i++)
            {
                insertBeginning(list, new Node(null, null, random.Next(0, 10000)));
            }

            sw.Reset();
            sw.Start();
            insertionSort(list);
            time = sw.ElapsedMilliseconds;
            Console.WriteLine(time.ToString() + "ms");

            //QUICKSORT

            Console.Write("Quick sort with 100 nodes takes ");
            for (int i = 0; i < 100; i++)
            {
                insertBeginning(list, new Node(null, null, random.Next(0, 10000)));
            }

            sw.Start();
            quickSort(list, random);
            time = sw.ElapsedMilliseconds;
            Console.WriteLine(time.ToString() + "ms");

            Console.Write("quick sort with 1000 nodes takes ");
            list = new DoubleLinkedList();
            for (int i = 0; i < 1000; i++)
            {
                insertBeginning(list, new Node(null, null, random.Next(0, 10000)));
            }

            sw.Reset();
            sw.Start();
            quickSort(list, random);
            time = sw.ElapsedMilliseconds;
            Console.WriteLine(time.ToString() + "ms");

            Console.Write("quick sort with 2000 nodes takes ");
            list = new DoubleLinkedList();
            for (int i = 0; i < 2000; i++)
            {
                insertBeginning(list, new Node(null, null, random.Next(0, 10000)));
            }

            sw.Reset();
            sw.Start();
            quickSort(list, random);
            time = sw.ElapsedMilliseconds;
            Console.WriteLine(time.ToString() + "ms");

            Console.Write("quick sort with 3000 nodes takes ");
            list = new DoubleLinkedList();
            for (int i = 0; i < 3000; i++)
            {
                insertBeginning(list, new Node(null, null, random.Next(0, 10000)));
            }

            sw.Reset();
            sw.Start();
            quickSort(list, random);
            time = sw.ElapsedMilliseconds;
            Console.WriteLine(time.ToString() + "ms");

            Console.Write("quick sort with 4000 nodes takes ");
            list = new DoubleLinkedList();
            for (int i = 0; i < 4000; i++)
            {
                insertBeginning(list, new Node(null, null, random.Next(0, 10000)));
            }

            sw.Reset();
            sw.Start();
            quickSort(list, random);
            time = sw.ElapsedMilliseconds;
            Console.WriteLine(time.ToString() + "ms");

            Console.Write("quick sort with 5000 nodes takes ");
            list = new DoubleLinkedList();
            for (int i = 0; i < 5000; i++)
            {
                insertBeginning(list, new Node(null, null, random.Next(0, 10000)));
            }

            sw.Reset();
            sw.Start();
            quickSort(list, random);
            time = sw.ElapsedMilliseconds;
            Console.WriteLine(time.ToString() + "ms");

            Console.ReadLine();
        }
    }
}
