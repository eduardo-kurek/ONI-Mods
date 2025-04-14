using UnityEngine;
using System.Text;

namespace CircuitNotIncluded;

public static class GameObjectDebugger
{
    public static void PrintGameObjectInfo(GameObject target, bool recursive = false, int maxDepth = 3){
        if (target == null){
            Debug.LogWarning("O GameObject fornecido é nulo!");
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"=== DEBUG INFO FOR: {target.name} ===");
        
        // Informações básicas
        sb.AppendLine($"Active: {target.activeSelf} (Hierarchy: {target.activeInHierarchy})");
        sb.AppendLine($"Tag: {target.tag}");
        sb.AppendLine($"Layer: {LayerMask.LayerToName(target.layer)} ({target.layer})");
        sb.AppendLine($"Static Flags: {target.isStatic}");
        sb.AppendLine($"Transform:");
        AppendTransformInfo(target.transform, sb);
        
        // Componentes
        sb.AppendLine("\nCOMPONENTS:");
        foreach (Component comp in target.GetComponents<Component>()){
            AppendComponentInfo(comp, sb);
        }
        
        // Hierarquia
        sb.AppendLine("\nHIERARCHY:");
        sb.AppendLine($"Parent: {(target.transform.parent != null ? target.transform.parent.name : "None")}");
        sb.AppendLine($"Child Count: {target.transform.childCount}");
        
        // Se recursivo, mostra informações dos filhos
        if (recursive && maxDepth > 0){
            sb.AppendLine("\nCHILDREN:");
            foreach (Transform child in target.transform){
                sb.AppendLine($"\n- Child: {child.name} -");
                PrintGameObjectInfo(child.gameObject, true, maxDepth - 1);
            }
        }
        
        Debug.Log(sb.ToString());
    }

    private static void AppendTransformInfo(Transform t, StringBuilder sb){
        sb.AppendLine($"  Position: {t.position}");
        sb.AppendLine($"  Local Position: {t.localPosition}");
        sb.AppendLine($"  Rotation: {t.rotation.eulerAngles}");
        sb.AppendLine($"  Local Rotation: {t.localRotation.eulerAngles}");
        sb.AppendLine($"  Scale: {t.localScale}");
        sb.AppendLine($"  Lossy Scale: {t.lossyScale}");
    }

    private static void AppendComponentInfo(Component comp, StringBuilder sb){
        if (comp == null){
            sb.AppendLine("  - [Missing Component]");
            return;
        }

        sb.Append($"  - {comp.GetType().Name}: ");
        sb.AppendLine();
    }

    // Versão simplificada para uso rápido
    public static void QuickDebug(GameObject target){
        PrintGameObjectInfo(target, false, 0);
    }
}