namespace CircuitNotIncluded.Grammar.Visitors.Expression;

public class ExpressionState(FilterState[] filters, BufferState[] buffers) {
	public FilterState[] Filters = filters;
	public BufferState[] Buffers = buffers;
}