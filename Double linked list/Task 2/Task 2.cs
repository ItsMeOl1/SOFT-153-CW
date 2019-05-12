using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Double_linked_list
{


    // single elements in the list 
    class Node
    {
        public char[] Forename { get; set; }    //
        public char[] Surname { get; set; }     //The data being help
        public int ID { get; set; }             //
        public Node Next { get; set; }  //The next node in the list
        public Node Previous { get; set; } //The previous node in the list

        public Node(Node next, Node prev, char[] fname, char[] sname, int id) //Can just pass through other nodes as it passes references, meaning there arent duplicates in memory
        {
            Next = next;
            Previous = prev;
            Forename = fname;
            Surname = sname;
            ID = id;
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
            StreamReader reader = new StreamReader("records.txt"); //open the txt
            DoubleLinkedList list = new DoubleLinkedList(); //initialise the list
            while (reader.EndOfStream == false) //untill the end of the txt
            {
                char[] line = reader.ReadLine().ToCharArray(); //get the line

                char[] forename = new char[10]; //seperate the forename into its own char[]
                int i = 0;
                while (line[i] != ',')
                {
                    forename[i] = line[i];
                    i++;
                }

                char[] surname = new char[10]; //seperate the surname into its own char[]
                i = i + 2;
                int offset = i;
                while (line[i] != ',')
                {
                    surname[i - offset] = line[i];
                    i++;
                }

                i = i + 2;
                offset = i;
                char[] ID = new char[line.Length - i]; //seperate the ID into its own char[], used to make sure IDs < 3 digets are copied properly
                while (i < line.Length)               
                {
                    ID[ID.Length - 1 - (i - offset)] = line[i]; //different to make sure the didgets come out in the right order
                    i++;
                }
                int id = 0;
                for (int o = 0; o < ID.Length; o++) //turn the ID char[] into an int
                {
                    id += (ID[o] - '0') * (int)Math.Pow(10, o);
                }

                insertEnd(list, new Node(null, null, forename, surname, id)); //add the current line/person to the list

            }
        }

        static void insertEnd(DoubleLinkedList l, Node newNode) //Inserts a node at the end of the list
        {
            if (l.lastNode == null)
            {
                l.firstNode = l.lastNode = newNode; //Make this node the only node
            }
            else
            {
                l.lastNode.Next = newNode; //add this node to the end of the list
                newNode.Previous = l.lastNode;
                l.lastNode = newNode; //reassign the last node
            }
        } 

        static int listLength(DoubleLinkedList l)//Gets the length of the list
        {
            int length = 0;
            Node node = l.firstNode;
            while (node != null) //go through each node till it ends
            {
                length++; //count each time
                node = node.Next;

            }
            return length;
        } 

        static void swapNodes(DoubleLinkedList l, Node node1, Node node2)//Swaps the position of two nodes in the list
        {
            if (node1.Previous == node2 || node1.Next == node2) // if the nodes are adjacent
            {
                Node nodeA, nodeB;
                if (node1.Previous == node2) //used for consistant naming
                {
                    nodeA = node2;
                    nodeB = node1;
                }
                else                        //used for consistant naming
                {
                    nodeA = node1;
                    nodeB = node2;
                }

                if (nodeA.Previous != null) //if the first of the two isn't the first overall
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
            } 
            else// if they arent next to each other
            {
                Node n1p = node1.Previous;
                Node n1n = node1.Next;

                node1.Previous = node2.Previous;
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

        static void printList(DoubleLinkedList l)//Displays the list
        {
            if (l.firstNode != null) //if there is something in the list
            {
                char[] arrow = new char[4] { ' ', '-', '>', ' ' }; //defines the arrow used in writing
                Node node = l.firstNode;
                Console.Write(node.Forename);
                Console.Write(' ');
                Console.Write(node.Surname);
                Console.Write(' ');
                Console.Write(node.ID);
                node = node.Next;
                while (node != null) //cycle through all nodes displaying their data
                {
                    Console.Write(arrow);
                    Console.Write(node.Forename);
                    Console.Write(' ');
                    Console.Write(node.Surname);
                    Console.Write(' ');
                    Console.Write(node.ID);
                    node = node.Next;
                }
                System.Console.WriteLine(' ');
            }
        } 

        static void insertionSort(DoubleLinkedList l, int sortBy) //Sorts the list //SortBy key: 0 = forename, 1 = surname, 2 = id
        {
            for (int index = 1; index <= listLength(l); index++)//go through each node
            {
                Node current = l.firstNode;
                int i = index;
                while (i > 1) //select the next node
                {
                    i--;
                    current = current.Next;
                }

                if (sortBy == 0) //if its sorting by first name
                {
                    while (current.Previous != null && compareNames(current.Previous.Forename, current.Forename)) //while its in the wrong position
                    {
                        swapNodes(l, current, current.Previous); //change the position
                    }
                }
                else if (sortBy == 1) //if its sorting by surname
                {
                    while (current.Previous != null && compareNames(current.Previous.Surname, current.Surname))//while its in the wrong position
                    {
                        swapNodes(l, current, current.Previous);//change the position
                    }
                }
                else if (sortBy == 2) //if its sorting by id
                {
                    while (current.Previous != null && current.Previous.ID > current.ID)//while its in the wrong position
                    {
                        swapNodes(l, current, current.Previous);//change the position
                    }
                }

            }
        }

        static bool compareNames(char[] name1, char[] name2)// Used to sort by names //true if name1 > name2
        {
            int letter = 0;//which letter its comparing
            int output = 0; //0 = unfound, 1 == return true, 2 == return false
            while (output == 0 && name1.Length > letter && name2.Length > letter) //while it doesnt know which is first
            {

                if (name1[letter] > name2[letter]) //if name1 is first
                {
                    output = 1;
                }
                else if (name1[letter] < name2[letter])//if name2 is first
                {
                    output = 2;
                }
                letter++; //cycle to the next letter
            }
            if (output == 1)
            {
                return true;
            }
            return false;
        }
    }
}

