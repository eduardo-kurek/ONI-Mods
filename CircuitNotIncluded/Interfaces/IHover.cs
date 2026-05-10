namespace CircuitNotIncluded.Interfaces;

public interface IHover {
	void OnHover(string circuitName, HoverTextDrawer drawer, SelectToolHoverTextCard cfg);
}