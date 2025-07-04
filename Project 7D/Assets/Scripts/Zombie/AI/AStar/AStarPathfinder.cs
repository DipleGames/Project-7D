using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarPathfinder
{
    private const float STRAIGHT_COST = 10f;
    private const float DIAGONAL_COST = 14f; // √2 * 10 (근사값)
    
    private readonly GridManager grid;
    private readonly Dictionary<Vector2Int, Node> openSet;
    private readonly HashSet<Vector2Int> closedSet;

    public AStarPathfinder(GridManager gridManager)
    {
        grid = gridManager;
        openSet = new Dictionary<Vector2Int, Node>();
        closedSet = new HashSet<Vector2Int>();
    }

    public List<Node> FindPath(Vector2Int startPos, Vector2Int endPos)
    {
        if (!IsValidPosition(startPos) || !IsValidPosition(endPos))
            return null;

        Node startNode = grid.GetNode(startPos);
        Node endNode = grid.GetNode(endPos);

        if (startNode == null || endNode == null || !endNode.isWalkable)
            return null;

        // 경로 탐색 초기화
        openSet.Clear();
        closedSet.Clear();

        startNode.gCost = 0;
        startNode.hCost = CalculateHeuristic(startPos, endPos);
        startNode.parent = null;
        
        openSet.Add(startPos, startNode);

        while (openSet.Count > 0)
        {
            // 가장 낮은 fCost를 가진 노드 찾기
            Node current = GetLowestFCostNode();
            Vector2Int currentPos = current.position;

            if (current == endNode)
                return ReconstructPath(current);

            openSet.Remove(currentPos);
            closedSet.Add(currentPos);

            foreach (Node neighbor in grid.GetNeighbors(current))
            {
                Vector2Int neighborPos = neighbor.position;
                if (closedSet.Contains(neighborPos)) continue;
                if (!neighbor.isWalkable) continue;

                float tentativeGCost = current.gCost + CalculateDistance(current, neighbor);

                if (tentativeGCost < neighbor.gCost || !openSet.ContainsKey(neighborPos))
                {
                    neighbor.gCost = tentativeGCost;
                    neighbor.hCost = CalculateHeuristic(neighborPos, endPos);
                    neighbor.parent = current;

                    if (!openSet.ContainsKey(neighborPos))
                        openSet.Add(neighborPos, neighbor);
                }
            }
        }

        return null; // 경로를 찾을 수 없음
    }

    private bool IsValidPosition(Vector2Int pos)
    {
        return pos.x >= 0 && pos.y >= 0; // 추가적인 범위 체크가 필요하다면 여기에 추가
    }

    private Node GetLowestFCostNode()
    {
        Node lowestNode = null;
        float lowestFCost = float.MaxValue;

        foreach (var node in openSet.Values)
        {
            if (node.fCost < lowestFCost || 
                (node.fCost == lowestFCost && node.hCost < lowestNode?.hCost))
            {
                lowestNode = node;
                lowestFCost = node.fCost;
            }
        }

        return lowestNode;
    }

    private List<Node> ReconstructPath(Node endNode)
    {
        List<Node> path = new();
        Node current = endNode;
        
        while (current != null)
        {
            path.Add(current);
            current = current.parent;
        }
        
        path.Reverse();
        return path;
    }

    /// <summary>
    /// 휴리스틱 비용을 계산합니다. 대각선 이동을 고려한 옥타일 거리를 사용합니다.
    /// </summary>
    private float CalculateHeuristic(Vector2Int a, Vector2Int b)
    {
        int dx = Mathf.Abs(a.x - b.x);
        int dy = Mathf.Abs(a.y - b.y);
        return STRAIGHT_COST * (dx + dy) + (DIAGONAL_COST - 2 * STRAIGHT_COST) * Mathf.Min(dx, dy);
    }

    /// <summary>
    /// 두 노드 사이의 실제 이동 비용을 계산합니다.
    /// 대각선 이동은 14, 직선 이동은 10의 비용이 듭니다.
    /// </summary>
    private float CalculateDistance(Node a, Node b)
    {
        int dx = Mathf.Abs(a.position.x - b.position.x);
        int dy = Mathf.Abs(a.position.y - b.position.y);
        return (dx == 1 && dy == 1) ? DIAGONAL_COST : STRAIGHT_COST;
    }
}
