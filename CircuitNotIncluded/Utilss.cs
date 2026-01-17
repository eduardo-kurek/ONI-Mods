using System.Text;
using Antlr4.Runtime;
using CircuitNotIncluded.Grammar;
using UnityEngine;
using static CircuitNotIncluded.Grammar.ExpressionParser;

namespace CircuitNotIncluded;

public class Utilss {
	public static void AddBuildingStrings(string buildingId, string name, string description, string effect){
			Strings.Add(new string[] {
				"STRINGS.BUILDINGS.PREFABS." + buildingId.ToUpperInvariant() + ".NAME",
				STRINGS.UI.FormatAsLink(name, buildingId)
			});
			Strings.Add(new string[] {
				"STRINGS.BUILDINGS.PREFABS." + buildingId.ToUpperInvariant() + ".DESC",
				description
			});
			Strings.Add(new string[] {
				"STRINGS.BUILDINGS.PREFABS." + buildingId.ToUpperInvariant() + ".EFFECT",
				effect
			});
		}

		public static void AddPlan(HashedString category, string subcategory, string idBuilding, string addAfter = null){
			const string str = "Adding ";
			const string str2 = " to category ";
			var hashedString = category;
			Debug.Log(str + idBuilding + str2 + hashedString.ToString());
			foreach(var planInfo in TUNING.BUILDINGS.PLANORDER){
				if(planInfo.category == category){
					Utilss.AddPlanToCategory(planInfo, subcategory, idBuilding, addAfter);
					return;
				}
			}

			Debug.Log(string.Format("Unknown build menu category: ${0}", category));
		}

		private static void AddPlanToCategory(PlanScreen.PlanInfo menu, string subcategory, string idBuilding,
			string addAfter = null){
			var buildingAndSubcategoryData = menu.buildingAndSubcategoryData;
			if(buildingAndSubcategoryData != null){
				if(addAfter == null){
					buildingAndSubcategoryData.Add(new KeyValuePair<string, string>(idBuilding, subcategory));
					return;
				}

				var num = buildingAndSubcategoryData.IndexOf(new KeyValuePair<string, string>(addAfter, subcategory));
				if(num == -1){
					Debug.Log(string.Concat(new string[] {
						"Could not find building ",
						subcategory,
						"/",
						addAfter,
						" to add ",
						idBuilding,
						" after. Adding at the end !"
					}));
					buildingAndSubcategoryData.Add(new KeyValuePair<string, string>(idBuilding, subcategory));
					return;
				}

				buildingAndSubcategoryData.Insert(num + 1, new KeyValuePair<string, string>(idBuilding, subcategory));
			}
		}

		public static ProgramContext Parse(string expression){
			AntlrInputStream inputStream = new(expression);
			ExpressionLexer lexer = new(inputStream);
			CommonTokenStream tokens = new(lexer);
			ExpressionParser parser = new(tokens);
			
			var syntaxAnalyzer = new SyntaxAnalyzer();
			parser.RemoveErrorListeners();
			parser.AddErrorListener(syntaxAnalyzer);
			
			ProgramContext tree = parser.program();
			
			syntaxAnalyzer.ThrowIfErrors();
    
			return tree;
		}
		
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