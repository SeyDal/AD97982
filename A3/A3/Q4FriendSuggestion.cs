using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q4FriendSuggestion : Processor
    {
        public Q4FriendSuggestion(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long, long[][], long[]>)Solve);
        public long extraxtMin(List<Node> stack)
        {
            int min = int.MaxValue;
            long minV = int.MaxValue;
            for (int i = 0; i < stack.Count; i++)
            {
                if (stack[i].value1 < minV)
                {
                    min = i;
                    minV = stack[i].value1;
                }

            }
            return min;
        }
        public long extraxtMin2(List<Node> stack)
        {
            int min = int.MaxValue;
            long minV = int.MaxValue;
            for (int i = 0; i < stack.Count; i++)
            {
                if (stack[i].value2 < minV)
                {
                    minV = stack[i].value2;
                    min = i;
                }
            }
            return min;
        }
        public long[] Solve(long NodeCount, long EdgeCount,
                              long[][] edges, long QueriesCount,
                              long[][] Queries)
        {
            List<List<long>> costs = new List<List<long>>();
            List<long> result = new List<long>();
            List<Node> graph = new List<Node>();

            for (int i = 0; i <= (int)NodeCount; i++)
            {
                graph.Add(new Node());
                graph[i].index = i;
            }
            for (int i = 0; i < edges.Length; i++)
            {
                graph[(int)edges[i][0]].edges.Add(graph[(int)edges[i][1]]);
                graph[(int)edges[i][0]].edgeCosts.Add(edges[i][2]);

                graph[(int)edges[i][1]].edges_in.Add(graph[(int)edges[i][0]]);
                graph[(int)edges[i][1]].edge_inCosts.Add(edges[i][2]);
            }
            for (int k = 0; k < QueriesCount; k++)
            {
                for (int i = 0; i <= (int)NodeCount; i++)
                {
                    graph[i].value1 = int.MaxValue;
                    graph[i].value2 = int.MaxValue;
                }

                MinHeap2 stack = new MinHeap2(EdgeCount);
                MinHeap2 stack2 = new MinHeap2(EdgeCount);
                List<Node> pro = new List<Node>();
                List<Node> pro2 = new List<Node>();
                graph[(int)Queries[k][1]].value2 = 0;
                graph[(int)Queries[k][0]].value1 = 0;

                stack.Add(Tuple.Create((int)Queries[k][0], graph[(int)Queries[k][0]].value1));
                stack2.Add(Tuple.Create((int)Queries[k][1], graph[(int)Queries[k][1]].value2));
                Node theNode = null;
                long minDistance = int.MaxValue;
                long counter = 0;
                while (stack._size != 0 && stack2._size != 0)
                {
                    counter++;
                    //long min = extraxtMin(stack);
                    //long min2 = extraxtMin2(stack2);
                    var node2Index = stack2.Pop();
                    var nodeIndex = stack.Pop();
                    Node node2 = graph[node2Index.Item1];
                    Node node = graph[nodeIndex.Item1];
                    if (node2.value2 != node2Index.Item2)
                    {
                        node2 = graph[stack2.Pop().Item1];
                    }
                    if (node.value1 != nodeIndex.Item2)
                    {
                        node = graph[stack.Pop().Item1];
                    }
                    //stack2.RemoveAt((int)min2);
                    //stack.RemoveAt((int)min);
                    pro.Add(node);
                    pro2.Add(node2);

                    for (int i = 0; i < node2.edges_in.Count; i++)
                    {
                        if (node2.value2 + node2.edge_inCosts[i] < node2.edges_in[i].value2)
                        {
                            node2.edges_in[i].value2 = node2.value2 + node2.edge_inCosts[i];
                            stack2.Add(Tuple.Create(node2.edges_in[i].index, node2.edges_in[i].value2));

                        }

                    }


                    for (int i = 0; i < node.edges.Count; i++)
                    {
                        if (node.value1 + node.edgeCosts[i] < node.edges[i].value1)
                        {
                            node.edges[i].value1 = node.value1 + node.edgeCosts[i];
                            stack.Add(Tuple.Create(node.edges[i].index, node.edges[i].value1));

                        }

                    }

                    if (node.value2 != int.MaxValue)
                    {
                        if (node.value2 + node.value1 < minDistance)
                            minDistance = node.value2 + node.value1;
                    }
                    if (node2.value1 != int.MaxValue)
                    {
                        if (node2.value2 + node2.value1 < minDistance)
                            minDistance = node2.value2 + node2.value1;
                    }

                    if (pro.Contains(node) && pro2.Contains(node) && node.value1 + node.value2 <= minDistance)
                    {
                        theNode = node;
                        break;
                    }
                    if (pro.Contains(node2) && pro2.Contains(node2) && node2.value2 + node2.value1 <= minDistance)
                    {

                        theNode = node2;
                        break;

                    }




                }


                if (theNode != null)
                {
                    long distance = theNode.value1 + theNode.value2;
                    if (graph[(int)Queries[k][0]].value2 < distance)
                        distance = graph[(int)Queries[k][0]].value2;
                    if (graph[(int)Queries[k][1]].value1 < distance)
                        distance = graph[(int)Queries[k][1]].value1;
                    result.Add(distance);
                }
                else
                    result.Add(-1);


            }



            return result.ToArray();
        }
    }
    public class MinHeap2
    {
        public Tuple<int, long>[] _elements;
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
