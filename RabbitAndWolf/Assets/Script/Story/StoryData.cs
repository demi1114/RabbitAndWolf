using UnityEngine;

[CreateAssetMenu(menuName = "Story/StoryData")]
public class StoryData : ScriptableObject
{
    [TextArea(2, 5)]
    public string[] lines;
}
