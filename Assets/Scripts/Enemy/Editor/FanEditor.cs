using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyController))]
public class FanEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyController enemyController = (EnemyController)target;

        // 扇型の扇形をギズモで表示
        Vector3 from = Quaternion.Euler(0, -enemyController.Data.FanAngle / 2, 0) * enemyController.transform.forward;
        Handles.color = new Color(1, 0, 0, 0.2f); // 半透明の赤色
        Handles.DrawSolidArc(enemyController.transform.position,
            Vector3.up, from, enemyController.Data.FanAngle, enemyController.Data.MoveDistance);

        // シーンビューでギズモを確認するための再描画
        if (GUI.changed)
        {
            EditorUtility.SetDirty(enemyController);
        }
    }
}
