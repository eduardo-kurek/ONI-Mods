using CircuitNotIncluded.Core;
using CircuitNotIncluded.Core.DTO;
using PeterHan.PLib.Core;
using PeterHan.PLib.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.UI;

public class CircuitSideScreen : SideScreenContent {
   private const float PORTS_VIEWPORT_HEIGHT = 220f;
   private Circuit? circuit = null;
   private TextMeshProUGUI circuitName = null!;
   private GameObject portsContainer = null!;
   private bool builded = false;
   
   protected override void OnPrefabInit(){
     base.OnPrefabInit();
     Build();
     Refresh();
   }

   private void Build(){
     BuildContentContainer();
     BuildCircuitName();
     BuildPortsContainer();
     BuildEditButton();
     builded = true;
   }

   private void BuildContentContainer(){
     ContentContainer = new GameObject("ContentContainer");
     ContentContainer.SetParent(gameObject);
     var layout = ContentContainer.AddComponent<VerticalLayoutGroup>();
     layout.spacing = 5;
     layout.padding = new RectOffset(10, 10, 10, 10);
     layout.childAlignment = TextAnchor.MiddleCenter;
     
     var rt = ContentContainer.GetComponent<RectTransform>();
     rt.offsetMax = new Vector2(10, 10);
     rt.offsetMin = new Vector2(10, 10);
   }

   private void BuildCircuitName(){
     var layout = ContentContainer.GetComponent<VerticalLayoutGroup>().transform;
     var go = Utils.UI.PinkText("", layout);
     circuitName = go.GetComponent<TextMeshProUGUI>();
     circuitName.fontSize = 16;
     circuitName.alignment = TextAlignmentOptions.Center;
   }

   private void BuildPortsContainer(){
     var parent = ContentContainer.GetComponent<VerticalLayoutGroup>();
     var scrollArea = new GameObject("PortsScrollArea");
     scrollArea.transform.SetParent(parent.transform, false);
     var areaLayout = scrollArea.AddComponent<LayoutElement>();
     areaLayout.minHeight = PORTS_VIEWPORT_HEIGHT;
     areaLayout.preferredHeight = PORTS_VIEWPORT_HEIGHT;
     areaLayout.flexibleHeight = 0;

     var portsComponent = new PortsContainerComponent();
     var scrollPane = new PScrollPane("PortsScroll") {
       ScrollHorizontal = false,
       ScrollVertical = true,
       AlwaysShowHorizontal = false,
       AlwaysShowVertical = false,
       Child = portsComponent,
       FlexSize = Vector2.right,
       TrackSize = 5
     }.Build();
     scrollPane.transform.SetParent(scrollArea.transform, false);
     PUIElements.SetAnchors(scrollPane, PUIAnchoring.Stretch, PUIAnchoring.Stretch);
     portsContainer = portsComponent.Container;
   }
   
   private void ClearContent(){ 
     foreach(Transform child in portsContainer.transform)
       Destroy(child.gameObject);
   }

   private void BuildContent(){
     var portsByCategory = circuit!.dto.Ports
       .GroupBy(p => p.Category)
       .OrderBy(g => g.Key);

     foreach(var category in portsByCategory){
       if(!category.Any()) continue;
       
       string categoryLabel = category.Key switch {
         PortCategory.Input => "Inputs",
         PortCategory.Output => "Outputs",
         _ => throw new InvalidOperationException("Category label not implemented")
       };
       Utils.UI.DarkText(categoryLabel + "\n", portsContainer.transform);

       var listingLayout = BuildListingLayout();
       foreach(PortDTO port in category){
         var go = Utils.UI.DarkText(port.GetDisplayText() + "\n", listingLayout.transform);
         var text = go.GetComponent<TextMeshProUGUI>();
         text.color = new Color(0.15f, 0.15f, 0.15f);
       }
     }
   }
   
   private VerticalLayoutGroup BuildListingLayout(){
     GameObject panel = new GameObject("Listing");
     panel.transform.SetParent(portsContainer.transform);
     var layout = panel.AddComponent<VerticalLayoutGroup>();
     layout.spacing = 5;
     layout.childAlignment = TextAnchor.UpperLeft;
     return layout;
   }

   private void BuildEditButton(){
     PButton editButton = new() {
       Text = "Edit",
       OnClick = OnEditButtonClicked
     };
     editButton.AddTo(ContentContainer);
   }

   private void OnEditButtonClicked(GameObject source){
     CircuitScreenManager.Instance.Build(circuit!);
   }
   
   public override void SetTarget(GameObject target){
     circuit = target.GetComponent<Circuit>();
     Refresh();
   }
   
   private void Refresh(){
     if(circuit == null) return;
     if(!builded) return;
     UpdateData();
   }

   private void UpdateData(){
     circuitName.text = circuit!.dto.Name;
     ClearContent();
     BuildContent();
   }

   public override bool IsValidForTarget(GameObject target){
     return target.GetComponent<Circuit>() != null;
   }

   public override string GetTitle() => "Circuit properties";

   private sealed class PortsContainerComponent : IUIComponent {
     public string Name => "PortsContainer";

     public event PUIDelegates.OnRealize OnRealize = null!;

     public GameObject Container { get; private set; } = null!;

     public GameObject Build(){
       Container = new GameObject("PortsContainer");
       var width = Container.AddComponent<LayoutElement>();
       width.minWidth = 0;
       width.preferredWidth = 0;
       width.flexibleWidth = 1;
       width.layoutPriority = 2;
       Container.AddComponent<VerticalLayoutGroup>();
       var layout = Container.GetComponent<VerticalLayoutGroup>();
       layout.spacing = 5;
       layout.childAlignment = TextAnchor.UpperLeft;
       layout.padding = new RectOffset(5, 0, 0, 0);
       OnRealize?.Invoke(Container);
       return Container;
     }
   }
}
