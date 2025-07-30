using CircuitNotIncluded.UI.Cells;
using PeterHan.PLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CircuitNotIncluded.Utils;

public static class Extensions
{
	public static GameObject AddOutline(this GameObject go){
		var outline = go.AddComponent<Outline>();
		outline.effectColor = UnityEngine.Color.black;
		outline.effectDistance = new Vector2(1, 1);
		return go;
	}
	
	public static VerticalLayoutGroup VerticalLayoutGroup(this GameObject go){
		return go.AddOrGet<VerticalLayoutGroup>();
	}

	public static VerticalLayoutGroup Spacing(this VerticalLayoutGroup layout, int spacing){
		layout.spacing = spacing;
		return layout;
	}
	
	public static LayoutElement LayoutElement(this GameObject go){
		return go.AddOrGet<LayoutElement>();
	}
	
	public static LayoutElement FlexibleWidth(this LayoutElement le, float value){
		le.flexibleWidth = value;
		return le;
	}
	
	public static LayoutElement FlexibleHeight(this LayoutElement le, float value){
		le.flexibleHeight = value;
		return le;
	}
	public static LayoutElement PreferredWidth(this LayoutElement le, float value){
		le.preferredWidth = value;
		return le;
	}
	
	public static LayoutElement PreferredHeight(this LayoutElement le, float value){
		le.preferredHeight = value;
		return le;
	}
	
	public static Image Image(this GameObject go){
		return go.AddOrGet<Image>();
	}
	
	public static Image Color(this Image img, Color color){
		img.color = color;
		return img;
	}
	
	public static CircuitCell CircuitCell(this GameObject go){
		return go.AddOrGet<CircuitCell>();
	}
	
	public static GridLayoutGroup GridLayoutGroup(this GameObject go){
		return go.AddOrGet<GridLayoutGroup>();
	}
	
	public static GridLayoutGroup CellSize(this GridLayoutGroup grid, float x, float y){
		grid.cellSize = new Vector2(x, y);
		return grid;
	}
	
	public static GridLayoutGroup Spacing(this GridLayoutGroup grid, float x, float y){
		grid.spacing = new Vector2(x, y);
		return grid;
	}
	
	public static GridLayoutGroup Padding(this GridLayoutGroup grid, int value){
		grid.padding = new RectOffset(value, value, value, value);
		return grid;
	}
	
	public static GridLayoutGroup ChildAlignment(this GridLayoutGroup grid, TextAnchor value){
		grid.childAlignment = value;
		return grid;
	}
	
	public static GridLayoutGroup StartCorner(this GridLayoutGroup grid, GridLayoutGroup.Corner corner){
		grid.startCorner = corner;
		return grid;
	}
	
	public static GridLayoutGroup StartAxis(this GridLayoutGroup grid, GridLayoutGroup.Axis axis){
		grid.startAxis = axis;
		return grid;
	}

	public static RectTransform RectTransform(this GameObject go){
		return go.AddOrGet<RectTransform>();
	} 

	public static RectTransform SizeDelta(this RectTransform rt, int x, int y){
		rt.sizeDelta = new Vector2(x, y);
		return rt;
	}
	
	public static RectTransform AnchorMin(this RectTransform rt, float x, float y){
		rt.anchorMin = new Vector2(x, y);
		return rt;
	}
	
	public static RectTransform AnchorMax(this RectTransform rt, float x, float y){
		rt.anchorMax = new Vector2(x, y);
		return rt;
	}
	
	public static RectTransform OffsetMin(this RectTransform rt, float x, float y){
		rt.offsetMin = new Vector2(x, y);
		return rt;
	}
	
	public static RectTransform OffsetMax(this RectTransform rt, float x, float y){
		rt.offsetMin = new Vector2(x, y);
		return rt;
	}
	
	public static RectTransform LocalPosition(this RectTransform rt, Vector3 vec){
		rt.localPosition = vec;
		return rt;
	}
	
	public static RectTransform LocalScale(this RectTransform rt, Vector3 vec){
		rt.localScale = vec;
		return rt;
	}

	public static PPanel DynamicSize(this PPanel panel, bool value){
		panel.DynamicSize = value;
		return panel;
	}
	
	public static PPanel FlexSize(this PPanel panel, float x, float y){
		panel.FlexSize = new Vector2(x, y);
		return panel;
	}
	
	public static PPanel Direction(this PPanel panel, PanelDirection direction){
		panel.Direction = direction;
		return panel;
	}
	
	public static PPanel BackColor(this PPanel panel, Color color){
		panel.BackColor = color;
		return panel;
	}
	
	public static PPanel Margin(this PPanel panel, int value){
		panel.Margin = new RectOffset(value, value, value, value);
		return panel;
	}
	
	public static PPanel Alignment(this PPanel panel, TextAnchor alignment){
		panel.Alignment = alignment;
		return panel;
	}
	
	public static PButton Text(this PButton button, string text){
		button.Text = text;
		return button;
	}
	
	public static PButton SetOnClick(this PButton button, PUIDelegates.OnButtonPressed onClick){
		button.OnClick = onClick;
		return button;
	}
	
	public static PPanel Spacing(this PPanel panel, int value){
		panel.Spacing = value;
		return panel;
	}

	public static T Text<T>(this T component, string value) where T : PTextComponent {
		component.Text = value;
		return component;
	}
	
	public static T Margin<T>(this T component, int value) where T : PTextComponent {
		component.Margin = new RectOffset(value, value, value, value);
		return component;
	}
	
	public static T Margin<T>(this T component, int x, int y) where T : PTextComponent {
		component.Margin = new RectOffset(x, x, y, y);
		return component;
	}
	
	public static T Margin<T>(this T component, int left, int right, int top, int bottom) where T : PTextComponent {
		component.Margin = new RectOffset(left, right, top, bottom);
		return component;
	}
	
	public static T Style<T>(this T component, TextStyleSetting style) where T : PTextComponent {
		component.TextStyle = style;
		return component;
	}
	
	public static T Parent<T>(this T component, PPanel panel) where T : PTextComponent {
		panel.AddChild(component);
		return component;
	}
	
	public static T FlexSize<T>(this T component, float x, float y) where T : PTextComponent {
		component.FlexSize = new Vector2(x, y);
		return component;
	}

}