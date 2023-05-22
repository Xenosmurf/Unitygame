using UnityEngine;

public interface IBuildingState
{
    bool IsRemove();
    void EndState();
    void OnAction(Vector3Int gridPosition);
    void UpdateState(Vector3Int gridPosition);
}