using System;
using System.Collections.Generic;
using System.Linq;

public class Paintball
{
    private static int _n;
    private static int _m;
    private static Node _source;
    private static Node _sink;
    private static Node[] _playersToShoot;
    private static Node[] _playersToBeShot;
    private static int[] _results;

    public static void Main(string[] args)
    {
        var lineArr = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
        _n = lineArr[0];
        _m = lineArr[1];
        ReadInputNodesAndBuildNetwork();
        if (_n > _m)
        {
            Console.WriteLine("Impossible");
            return;
        }

        AddSourceAndSinkToNetwork();
        AugmentNetwork();
        OrderResultForOutput();
        PrintOutput();
    }

    private static void ReadInputNodesAndBuildNetwork()
    {
        _playersToShoot = new Node[_n];
        _playersToBeShot = new Node[_n];
        for (int i = 0; i < _n; i++) _playersToShoot[i] = new Node(i);
        for (int i = 0; i < _n; i++) _playersToBeShot[i] = new Node(i);

        for (int i = 0; i < _m; i++)
        {
            var lineArr = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
            var p1ToShoot = _playersToShoot[lineArr[0] - 1];
            var p1ToBeShot = _playersToBeShot[lineArr[0] - 1];
            var p2ToShoot = _playersToShoot[lineArr[1] - 1];
            var p2ToBeShot = _playersToBeShot[lineArr[1] - 1];

            p1ToShoot.OutgoingEdges.Add(new Edge(p1ToShoot, p2ToBeShot, 1));
            p2ToShoot.OutgoingEdges.Add(new Edge(p2ToShoot, p1ToBeShot, 1));

            p2ToBeShot.OutgoingEdges.Add(new Edge(p2ToBeShot, p1ToShoot, 0, true));
            p1ToBeShot.OutgoingEdges.Add(new Edge(p1ToBeShot, p2ToShoot, 0, true));
        }
    }

    private static void AddSourceAndSinkToNetwork()
    {
        _source = new Node(-1);
        _sink = new Node(-2);
        for (int i = 0; i < _n; i++)
        {
            _source.OutgoingEdges.Add(new Edge(_source, _playersToShoot[i], 1));
            _playersToShoot[i].OutgoingEdges.Add(new Edge(_playersToShoot[i], _source, 1, true));

            _playersToBeShot[i].OutgoingEdges.Add(new Edge(_playersToBeShot[i], _sink, 1));
            _sink.OutgoingEdges.Add(new Edge(_sink, _playersToBeShot[i], 1, true));
        }
    }

    private static void AugmentNetwork()
    {
        while (true) //augment by finding best path
        {
            var queue = new Queue<(Node, Node)>();
            var visited = new HashSet<Node>();
            var previous = new Dictionary<Node, Node>();
            queue.Enqueue((_source, null));
            Node currentNode;
            Node previousNode;
            do //find best path - breadth first search
            {
                (currentNode, previousNode) = queue.Dequeue();
                if (visited.Contains(currentNode)) continue;
                previous.Add(currentNode, previousNode);
                if (currentNode == _sink) break;
                visited.Add(currentNode);

                foreach (var e in currentNode.OutgoingEdges)
                {
                    if (e.Capacity > 0)
                    {
                        queue.Enqueue((e.To, currentNode));
                    }
                }
            } while (queue.Count > 0);

            if (currentNode != _sink) break;

            //push flow through this path
            currentNode = _sink;
            while (currentNode != _source)
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
    }

    private static void FindPossiblePath()
    {

    }

    private static void PushFlow()
    {

    }

    private static void OrderResultForOutput()
    {
        _results = new int[_n];
        foreach (var e1 in _source.OutgoingEdges)
        {
            foreach (var e2 in e1.To.OutgoingEdges)
            {
                if (e2.Capacity == 0 && !e2.Residual)
                {
                    _results[e2.From.Id] = e2.To.Id + 1;
                }
            }
        }
    }

    private static void PrintOutput()
    {
        if (_results.Any(x => x == 0))
        {
            Console.WriteLine("Impossible");
            return;
        }

        for (int i = 0; i < _results.Length; i++) Console.WriteLine(_results[i]);
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