namespace Was.ViewScripts.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    
    internal class Graph<T>
    {
        private readonly IDictionary<T, IList<T>> adjacencyList;

        public Graph()
        {
            this.adjacencyList = new Dictionary<T, IList<T>>();
        }

        public void AddEdge(T v1, T v2)
        {
            if (!this.adjacencyList.ContainsKey(v2))
            {
                this.adjacencyList.Add(v2, new List<T>());
            }

            if (this.adjacencyList.ContainsKey(v1))
            {
                this.adjacencyList[v1].Add(v2);
            }
            else
            {
                this.adjacencyList.Add(v1, new List<T> {v2});
            }
        }

        public IEnumerable<T> SortTopological()
        {       
            // TODO perfrom     
            var adjList = this.adjacencyList.ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach (var k in adjList.Keys.ToList())
            {
                adjList[k] = adjList[k].ToList();
            }

            var incomingEdges = adjList.ToDictionary(p => p.Key, p => 0);
            foreach (var v in adjList.Keys)
            {
                foreach (var n in adjList[v])
                    incomingEdges[n] += 1;
            }

            var temp = new Queue<T>(incomingEdges.Where(p => p.Value == 0).Select(p => p.Key));
            while (temp.Count > 0)
            {
                var v = temp.Dequeue();

                yield return v;

                foreach (var n in adjList[v].ToList())
                {
                    adjList[v].Remove(n);
                    incomingEdges[n] -= 1;
                    if (incomingEdges[n] == 0)
                        temp.Enqueue(n);
                }
            }

            if (adjList.Any(p => p.Value.Any()))
                throw new InvalidOperationException("Graph contains a cycle");
        } 

        public bool HasCycle()
        {
            var visited = new Dictionary<T, bool>();
            var buffer = new Dictionary<T, bool>();
            foreach (var key in this.adjacencyList.Keys)
            {
                visited.Add(key, false);
                buffer.Add(key, false);
            }

            return this.adjacencyList.Keys.Any(key => this.HasCycle(key, visited, buffer));
        }
        
        private bool HasCycle(T v, IDictionary<T, bool> visited, IDictionary<T, bool> buffer)
        {
            if (!visited[v])
            {
                visited[v] = true;
                buffer[v] = true;

                foreach (var neighbourhood in this.adjacencyList[v])
                {
                    if (!visited[neighbourhood] && this.HasCycle(neighbourhood, visited, buffer))
                    {
                        return true;
                    }

                    if (buffer[neighbourhood])
                    {
                        return true;
                    }
                }
            }

            buffer[v] = false;
            return false;
        }
    }
}
