namespace CircuitNotIncluded.Utils;

public class CNIUtil {
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
					CNIUtil.AddPlanToCategory(planInfo, subcategory, idBuilding, addAfter);
					return;
				}
			}

			Debug.Log($"Unknown build menu category: ${category}");
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

		public static void Log(string msg) => Debug.Log("[CNI] " + msg);
}