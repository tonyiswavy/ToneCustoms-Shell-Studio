namespace ToneCustoms.ShellStudio.Editor;
public enum GizmoAxis { None, X, Y, Z }
public sealed class GizmoState { public TransformMode Mode {get;set;}=TransformMode.Move;public GizmoAxis Hovered {get;set;}public GizmoAxis Active {get;set;}public bool LocalSpace {get;set;} }
