using Antlr4.Runtime;
using CircuitNotIncluded.Grammar;
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
	
}