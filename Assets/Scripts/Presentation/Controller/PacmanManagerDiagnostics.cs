using UnityEngine;

public class PacmanManagerDiagnostics : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log($"🌀 [Awake] GameObject: {gameObject.name} | ActiveInHierarchy: {gameObject.activeInHierarchy} | Component enabled: {enabled}");
    }

    private void OnEnable()
    {
        Debug.Log($"✅ [OnEnable] GameObject: {gameObject.name} | ActiveInHierarchy: {gameObject.activeInHierarchy}");
    }

    private void OnDisable()
    {
        Debug.Log($"❌ [OnDisable] GameObject: {gameObject.name}");
    }

    private void Start()
    {
        Debug.Log($"🚀 [Start] GameObject: {gameObject.name} | Component enabled: {enabled}");
    }

    private void Update()
    {
        Debug.Log($"📢 [Update] GameObject: {gameObject.name} | ActiveInHierarchy: {gameObject.activeInHierarchy} | enabled: {enabled}");
    }
}
