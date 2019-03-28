using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4
{
    public class Q3ComputeDistance : Processor
    {
        public Q3ComputeDistance(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long,long, long[][], long[][], long, long[][], long[]>)Solve);


        public long[] Solve(long nodeCount, 
                            long edgeCount,
                            long[][] points,
                            long[][] edges,
                            long queriesCount,
                            long[][] queries)
        {
            List<long> result = new List<long>();
            List<Node> graph = new List<Node>();
            graph.Add(new Node());
            for (int i = 1; i <= (int)nodeCount; i++)
            {
                graph.Add(new Node());
                graph[i].x = points[i - 1][0];
                graph[i].y = points[i - 1][1];
                graph[i].index = i;

            }
            for (int i = 0; i < edges.Length; i++)
            {
                graph[(int)edges[i][0]].edges.Add(graph[(int)edges[i][1]]);
                graph[(int)edges[i][0]].edgeCosts.Add(edges[i][2]);
            }
            for (int k = 0; k < queriesCount; k++)
            {
                int startNode = (int)queries[k][0];
                int endNode = (int)queries[k][1];
                for (int i = 1; i <= nodeCount; i++)
                {
                    graph[i].value = int.MaxValue;
                }
                MinHeap2 stack = new MinHeap2(nodeCount);
                graph[startNode].value = 0;
                stack.Add(Tuple.Create(startNode,graph[startNode].value));
                while (stack._size != 0)
                {
                    //long min = extraxtMin(stack);
                    //if (min == endNode)
                        //break;
                    var nodeIndex = stack.Pop();
                    Node node = graph[nodeIndex.Item1];
                    if (node == graph[endNode])
                        break;
                   // stack.RemoveAt((int)min);

                    for (int i = 0; i < node.edges.Count; i++)
                    {
                        if (node.value + (node.edgeCosts[i] - potentialFUNC(node,graph[endNode])+potentialFUNC(node.edges[i],graph[endNode])) < node.edges[i].value)
                        {
                            node.edges[i].value = node.value + (node.edgeCosts[i] - potentialFUNC(node, graph[endNode]) + potentialFUNC(node.edges[i], graph[endNode]));
                            stack.Add(Tuple.Create(node.edges[i].index, node.edges[i].value));
                        }
                    }
                }
                
                if (graph[endNode].value != int.MaxValue)
                    result.Add( graph[endNode].value+potentialFUNC(graph[startNode],graph[endNode]));
                else
                    result.Add(-1);
            }
            return result.ToArray();
        }
        public long potentialFUNC(Node u,Node t)
        {
            return (long)Math.Pow((Math.Pow(u.x-t.x,2)+Math.Pow(u.y-t.y,2)), 0.5);
        }
        public long extraxtMin(List<Node> stack)
        {
            int min = int.MaxValue;
            for (int i = 0; i < stack.Count; i++)
            {
                if (stack[i].value < min)
                    min = i;
            }
            return min;
        }
        
    }
    public class Node
    {
        public long value = int.MaxValue;
        public long valueInHeap = int.MaxValue;
        public int index;
        public List<Node> edges = new List<Node>();
        public List<long> edgeCosts = new List<long>();
        public long x;
        public long y;

    }
    public class MinHeap2
    {
        public Tuple<int , long>[] _elements;
        public int _size;

        public MinHeap2(long size)
        {
            _elements = new Tuple<int, long>[size];
        }

        private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
        private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
        private bool IsRoot(int elementIndex) => elementIndex == 0;

        private Tuple<int, long> GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
        private Tuple<int, long> GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
        private Tuple<int, long> GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

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

        public Tuple<int, long> Peek()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            return _elements[0];
        }

        public Tuple<int, long> Pop()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            var result = _elements[0];
            _elements[0] = _elements[_size - 1];
            _size--;

            ReCalculateDown();

            return result;
        }

        public void Add(Tuple<int, long> element)
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
                if (HasRightChild(index) && GetRightChild(index).Item2 < GetLeftChild(index).Item2)
                {
                    smallerIndex = GetRightChildIndex(index);
                }

                if (_elements[smallerIndex].Item2 >= _elements[index].Item2)
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
            while (!IsRoot(index) && _elements[index].Item2 < GetParent(index).Item2)
            {
                var parentIndex = GetParentIndex(index);
                Swap(parentIndex, index);
                index = parentIndex;
            }
        }
    }

}
