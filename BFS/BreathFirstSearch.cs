﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS
{
    internal class BreathFirstSearch
    {
        //Contains information of every node in the graph
        public List<Node> _nodeList = new List<Node>();
        int _id = 0;

        int n; //number of vertices
        int k; //number of edges
        string _startP, _endP; //startPoint and endPoint

        //Store the: {vertex name - its index in _nodeList}     
        Dictionary<string, int> mapVertices = new Dictionary<string, int>();

        List<string> path = new List<string>();
        public string stringToExport;

        List<string> visitedVertex = new List<string>();


		static int numOfDash = 33;
		string spacePerCell = string.Concat(Enumerable.Repeat(' ', numOfDash));
		string dashPerCell = string.Concat(Enumerable.Repeat('-', numOfDash));

		public BreathFirstSearch(string[] data)
        {

            n = int.Parse(data[0]); //number of vertices

            PreDefineMap(data[1]); // Define for the map that contains: <vertices name> : <its id>
            k = int.Parse(data[2]); //number of edges

            for (int i = 3; i < (3 + n); i++)
            {

                Node node = new Node();

                //eg: A B C D
                //data[i] = "A B C D\r"
                data[i] = data[i].Substring(0, data[i].Length - 1);
                data[i] = data[i].TrimEnd(' ');

                string[] strings = data[i].Split(new string[] { " " }, StringSplitOptions.None);

                //The current vertex: A
                node.Name = strings[0];

                //Add all the neighbor vertices into a list: C D E
                List<string> neighbors = new List<string>();

                for (int j = 1; j < strings.Length; j++)
                {
                    neighbors.Add(strings[j]);

                }

                //Save "all the neighbor vertices" in the "current vertex"
                node.Neighbors = neighbors;

                _nodeList.Add(node);
            }

            //start point and end point
            string[] tmp = data[n + 3].Split(new string[] { " " }, StringSplitOptions.None);
            _startP = tmp[0];
            _endP = tmp[1];


            /*Solve();*/
        }

        public void PreDefineMap(string data)
        {
            string[] nameList = data.Split(new string[] { " " }, StringSplitOptions.None);
            foreach (string name in nameList)
            {
                mapVertices[name] = _id;
                _id++;
            }

            _id = 0; //reset id for another purpose 
        }

        public bool Solve()
        {



            #region In tiêu đề
            string TrangThaiPhatTrien = "Trạng thái phát triển";
            string TrangThaiKe = "Trạng thái kề";
            string DanhSachL = "Danh sách L";
            string DinhDaDuyet = "Đỉnh đã duyệt";


			stringToExport += $"+{dashPerCell}+{dashPerCell}+{dashPerCell}+{dashPerCell}+\n";
            stringToExport += "+";
            stringToExport += string.Concat(Enumerable.Repeat(' ', (numOfDash - TrangThaiPhatTrien.Length) / 2));
            stringToExport += TrangThaiPhatTrien;
            stringToExport += string.Concat(Enumerable.Repeat(' ', (numOfDash - TrangThaiPhatTrien.Length) / 2));
            stringToExport += "+";


            stringToExport += string.Concat(Enumerable.Repeat(' ', (numOfDash - TrangThaiKe.Length) / 2));
            stringToExport += TrangThaiKe;
            stringToExport += string.Concat(Enumerable.Repeat(' ', (numOfDash - TrangThaiKe.Length) / 2));
            stringToExport += "+";


            stringToExport += string.Concat(Enumerable.Repeat(' ', (numOfDash - DanhSachL.Length) / 2));
            stringToExport += DanhSachL;
            stringToExport += string.Concat(Enumerable.Repeat(' ', (numOfDash - DanhSachL.Length) / 2));
			stringToExport += "+";

			stringToExport += string.Concat(Enumerable.Repeat(' ', (numOfDash - DinhDaDuyet.Length) / 2));
			stringToExport += DinhDaDuyet;
			stringToExport += string.Concat(Enumerable.Repeat(' ', (numOfDash - DinhDaDuyet.Length) / 2));

			stringToExport += "+\n";


            stringToExport += $"+{dashPerCell}+{dashPerCell}+{dashPerCell}+{dashPerCell}+\n";
			#endregion


			//To mark vertices approved
			bool[] visited = new bool[n];
            Queue<string> q = new Queue<string>();
            // Parent dictionary to store the parent of each node during BFS
            Dictionary<string, string> parent = new Dictionary<string, string>();

            //Put _startP in queue
            q.Enqueue(_startP);
            visited[mapVertices[_startP]] = true; //0 1 2 3 4 5 
            //visitedVertex.Add(_startP);


            while (q.Count > 0)
            {

                string curr = q.Dequeue();
                visitedVertex.Add(curr);
                Console.WriteLine($"curr: {curr}");

                foreach (string i in visitedVertex)
                {
                    Console.Write($"{i}, ");
                }
                Console.WriteLine();

                // Thêm thông tin về trạng thái phát triển
                stringToExport += "| ";
                stringToExport += curr.ToString();
                stringToExport += string.Concat(Enumerable.Repeat(' ', numOfDash - 2));


                if (curr == _endP)
                {
                    stringToExport += "| TTKT - Dừng";
                    stringToExport += string.Concat(Enumerable.Repeat(' ', numOfDash - 12));
                    stringToExport += $"|{spacePerCell}|";

					//In ra lần cuối 
					string InRaLanCuoi = "";
					foreach (string i in visitedVertex)
                    {
                        InRaLanCuoi += i;
                        InRaLanCuoi += ", ";
                    }

					if (InRaLanCuoi.Length > 2)
					{
						InRaLanCuoi = InRaLanCuoi.Substring(0, InRaLanCuoi.Length - 2);
					}

					stringToExport += InRaLanCuoi;
					stringToExport += string.Concat(Enumerable.Repeat(' ', numOfDash - InRaLanCuoi.Length));
                    stringToExport += "|\n";

					stringToExport += $"+{dashPerCell}+{dashPerCell}+{dashPerCell}+{dashPerCell}+\n";


                    RetrieveThePath(parent);
                    return true;
                }


                string adjacentList = "| ";

                foreach (string vertex in _nodeList[mapVertices[curr]].Neighbors)
                {
                    if (visited[mapVertices[vertex]] == false)
                    {
                        //Success
                        q.Enqueue(vertex);
                        visited[mapVertices[vertex]] = true;
                        parent[vertex] = curr; // Store the parent of vertex
                    }
                }

                adjacentList += _nodeList[mapVertices[curr]].ListNeighborsToString();

                stringToExport += adjacentList;
                stringToExport += string.Concat(Enumerable.Repeat(' ', numOfDash - adjacentList.Length + 1));
                stringToExport += "|";



                //Danh sách L 
                Queue<string> temp = new Queue<string>();
                string listL = " ";
                while (q.Count > 0)
                {
                    string a = q.Dequeue();
                    listL += $"{a}, ";

                    temp.Enqueue(a);
                    Console.WriteLine(a);
                }

                while (temp.Count > 0)
                {
                    q.Enqueue(temp.Dequeue());
                }

                listL = listL.Substring(0, listL.Length - 2);
                listL += string.Concat(Enumerable.Repeat(' ', numOfDash - listL.Length));
                stringToExport += listL;
				stringToExport += "|";


                PrintTheVisitedVertices();


				stringToExport += "|\n";
			}


            return false;
        }

        public void PrintTheVisitedVertices()
        {
			//Đỉnh đã duyệt
			string daDuyet = "";
			foreach (string a in visitedVertex)
			{
				daDuyet += a;
				daDuyet += ", ";
			}

			if (daDuyet.Length > 2)
			{
				daDuyet = daDuyet.Substring(0, daDuyet.Length - 2);
			}

			stringToExport += daDuyet;
			stringToExport += string.Concat(Enumerable.Repeat(' ', numOfDash - daDuyet.Length));
		}

        public void RetrieveThePath(Dictionary<string, string> parent)
        {
            string current = _endP;
            while (current != _startP)
            {
                path.Add(current);
                current = parent[current];
			}
            path.Add(_startP);
            path.Reverse(); // Reverse the path to get it from start to end

            string pathString = "";
            foreach (string i in path)
            {
                pathString += $"{i} -> ";
            }

            pathString = pathString.Substring(0, pathString.Length - 3);

            stringToExport += pathString;
        }

    }


}
