using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4
{
    public class Q1BuildingRoads : Processor
    {
        public Q1BuildingRoads(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], double>)Solve);


        public double Solve(long pointCount, long[][] points)
        {
            double cost = 0;
            MinHeap edges = new MinHeap(pointCount*pointCount);
            for (int i = 0; i < pointCount - 1; i++)
            {
                for (int j = i + 1; j < pointCount; j++)
                {
                    edge edge = new edge();
                    edge.u = i;
                    edge.v = j;
                    edge.cost = Math.Pow(Math.Pow((points[i][0] - points[j][0]), 2) + Math.Pow((points[i][1] - points[j][1]),2), 0.5);
                    edges.Add(edge);
                }
            }
           
            List<long> sets = new List<long>();
            for (int i = 0; i < pointCount ; i++)
            {
                sets.Add(i);
            }
            int edgeNumber = 0;
            int size = edges._size;
            for (int i = 0; i < size; i++)
            {
                if (edgeNumber == pointCount - 1)
                    break;
                edge edge = edges.Pop();
                if (sets[edge.u] != sets[edge.v])
                {
                    edgeNumber++;

                    cost += edge.cost;
                    long tmp = sets[edge.v];
                    for (int j = 0; j < pointCount; j++)
                    {
                        if (sets[j] == tmp)
                            sets[j] = sets[edge.u];
                    }
                }
            }
                /*List<Node> graph = new List<Node>();
                List<edge> edges = new List<edge>();

                for (int i=0; i<pointCount; i++)
                {
                    graph.Add(new Node());
                    graph[i].x = points[i][0];
                    graph[i].y = points[i][1];
                    graph[i].set = i;

                }


                for (int i=0; i < pointCount-1; i++)
                {
                    for (int j=i+1; j < pointCount;j++)
                    {
                        edge edge = new edge();
                        edge.u = graph[i];
                        edge.v = graph[j];
                        edge.cost = Math.Pow(Math.Pow((edge.u.x - edge.v.x), 2) + Math.Pow((edge.u.y - edge.v.y), 2), 0.5);
                        edges.Add(edge);
                    }
                }
                for (int i=0; i < edges.Count; i++)
                {
                    double min = double.MaxValue;
                    int minIndex = int.MaxValue;
                    for (int j=i; j < edges.Count; j++)
                    {
                        if (edges[j].cost < min)
                        {
                            minIndex = j;
                            min = edges[j].cost;
                        }
                    }
                    edge tmp = edges[i];
                    edges[i] = edges[minIndex];
                    edges[minIndex] = tmp;
                }
                double cost = 0;
                int edgeNumber = 0;

                for(int i= 0; i<edges.Count; i++)
                {
                    if (edgeNumber == pointCount - 1)
                        break;
                    if (edges[i].u.set != edges[i].v.set)
                    {
                        edgeNumber++;
                        edges[i].v.edges.Add(edges[i].u);
                        edges[i].u.edges.Add(edges[i].v);
                        cost += edges[i].cost;
                        long tmp = edges[i].v.set;
                        for (int j = 0; j < pointCount; j++)
                        {
                            if (graph[j].set == tmp)
                                graph[j].set = edges[i].u.set;
                        }
                    }

                }*/
                return (double)((long)(cost*1000000+0.5))/1000000;
        }
        
      
        public class Node
        {
            public long x;
            public long set;
            public long y;
            public List<Node> edges = new List<Node>(); 

        }
        public class edge
        {
            public int u;
            public int v;
            public double cost;
        }
        public class MinHeap
        {
            public edge[] _elements;
            public int _size;

            public MinHeap(long size)
            {
                _elements = new edge[size];
            }

            private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
            private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
            private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

            private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
            private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
            private bool IsRoot(int elementIndex) => elementIndex == 0;

            private edge GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
            private edge GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
            private edge GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

            private void Swap(int firstIndex, int secondIndex)
            {
                var temp = _elements[firstIndex];
                _elements[firstIndex] = _elements[secondIndex];
                _elements[secondIndex] = temp;
            }

            public bool IsEmpty()
            {
                return _size == 0;
            }

            public edge Peek()
            {
                if (_size == 0)
                    throw new IndexOutOfRangeException();

                return _elements[0];
            }

            public edge Pop()
            {
                if (_size == 0)
                    throw new IndexOutOfRangeException();

                var result = _elements[0];
                _elements[0] = _elements[_size - 1];
                _size--;

                ReCalculateDown();

                return result;
            }

            public void Add(edge element)
            {
                if (_size == _elements.Length)
                    throw new IndexOutOfRangeException();

                _elements[_size] = element;
                _size++;

                ReCalculateUp();
            }

            private void ReCalculateDown()
            {
                int index = 0;
                while (HasLeftChild(index))
                {
                    var smallerIndex = GetLeftChildIndex(index);
                    if (HasRightChild(index) && GetRightChild(index).cost < GetLeftChild(index).cost)
                    {
                        smallerIndex = GetRightChildIndex(index);
                    }

                    if (_elements[smallerIndex].cost >= _elements[index].cost)
                    {
                        break;
                    }

                    Swap(smallerIndex, index);
                    index = smallerIndex;
                }
            }

            private void ReCalculateUp()
            {
                var index = _size - 1;
                while (!IsRoot(index) && _elements[index].cost < GetParent(index).cost)
                {
                    var parentIndex = GetParentIndex(index);
                    Swap(parentIndex, index);
                    index = parentIndex;
                }
            }
        }
    }
}
