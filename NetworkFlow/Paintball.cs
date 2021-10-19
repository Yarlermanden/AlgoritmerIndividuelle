using System;
using System.Collections.Generic;
using System.Linq;
public class Paintball
{
    public static void Main(string[] args) {
        var lineArr = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
        var n = lineArr[0];
        var m = lineArr[1];

        var playersToShoot = new Node[n];
        var playersToBeShot = new Node[n];
        for (int i = 0; i < n; i++) playersToShoot[i] = new Node(i);
        for (int i = 0; i < n; i++) playersToBeShot[i] = new Node(i);

        for (int i = 0; i < m; i++)
        {
            lineArr = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
            var p1ToShoot = playersToShoot[lineArr[0]-1];
            var p1ToBeShot = playersToBeShot[lineArr[0]-1];
            var p2ToShoot = playersToShoot[lineArr[1]-1];
            var p2ToBeShot = playersToBeShot[lineArr[1]-1];
            
            p1ToShoot.OutgoingEdges.Add(new Edge(p1ToShoot, p2ToBeShot, 1));
            p2ToShoot.OutgoingEdges.Add(new Edge(p2ToShoot, p1ToBeShot, 1));
            
            p2ToBeShot.OutgoingEdges.Add(new Edge(p2ToBeShot, p1ToShoot, 0, true));
            p1ToBeShot.OutgoingEdges.Add(new Edge(p1ToBeShot, p2ToShoot, 0, true));
        }

        if (n > m)
        {
            Console.WriteLine("Impossible");
            return;
        }

        //Add from source to all playersToShoot
        //Add sink to all playersToBeShot
        var source = new Node(-1);
        var sink = new Node(-2);
        for (int i = 0; i < n; i++)
        {
            source.OutgoingEdges.Add(new Edge(source, playersToShoot[i], 1));
            playersToShoot[i].OutgoingEdges.Add(new Edge(playersToShoot[i], source, 1, true));
            
            playersToBeShot[i].OutgoingEdges.Add(new Edge(playersToBeShot[i], sink, 1));
            sink.OutgoingEdges.Add(new Edge(sink, playersToBeShot[i], 1, true));
        }


        while (true)    //augment by finding best path
        {
            var queue = new Queue<(Node, Node)>();
            var visited = new HashSet<Node>();
            var previous = new Dictionary<Node, Node>();
            queue.Enqueue((source, null));
            Node currentNode;
            Node previousNode;
            do //find best path - breadth first search
            {
                (currentNode, previousNode) = queue.Dequeue();
                if (visited.Contains(currentNode)) continue;
                previous.Add(currentNode, previousNode);
                if (currentNode == sink) break;
                visited.Add(currentNode);

                foreach (var e in currentNode.OutgoingEdges)
                {
                    if (e.Capacity > 0)
                    {
                        queue.Enqueue((e.To, currentNode)); 
                    }
                }
            } while (queue.Count > 0);

            if (currentNode != sink) break;
            
            //push flow through this path
            currentNode = sink;
            while (currentNode != source)
            {
                previousNode = previous[currentNode];
                var edge1 = previousNode.OutgoingEdges.Single(edge => edge.To == currentNode);
                edge1.Capacity -= 1;

                var residualEdge = currentNode.OutgoingEdges.Single(edge => edge.To == previousNode);
                residualEdge.Capacity += 1;
                //find the edge used

                currentNode = previousNode;
            }
        }

        //Run from source to sink.
        //All the edges in second round which capacity is 0 represents which nodes shot eachother
        var results = new int[n];
        foreach (var e1 in source.OutgoingEdges)
        {
            foreach (var e2 in e1.To.OutgoingEdges)
            {
                if (e2.Capacity == 0 && !e2.Residual)
                {
                    results[e2.From.Id] = e2.To.Id + 1;
                }
            }
        }

        if (results.Any(x => x == 0))
        {
            Console.WriteLine("Impossible");
            return;
        }
        for(int i = 0; i < results.Length; i++) Console.WriteLine(results[i]);
    }
}

public class Node
{
    public int Id { get; set; }
    public List<Edge> OutgoingEdges { get; set; }

    public Node(int id)
    {
        Id = id;
        OutgoingEdges = new List<Edge>();
    }
}

public class Edge
{
    public Node From { get; set; }
    public Node To { get; set; }
    public int Capacity { get; set; }
    public bool Residual { get; set; }

    public Edge(Node from, Node to, int capacity, bool residual = false)
    {
        From = from;
        To = to;
        Capacity = capacity;
        Residual = residual;
    }
}