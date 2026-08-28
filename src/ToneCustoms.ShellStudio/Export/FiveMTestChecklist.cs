namespace ToneCustoms.ShellStudio.Export;
public sealed record TestCheck(string Name,bool Required);
public sealed class FiveMTestChecklist { public IReadOnlyList<TestCheck> Checks {get;}=[new("Resource starts without errors",true),new("Shell renders at test placement",true),new("Floor collision supports player",true),new("Wall collision blocks player",true),new("Door/window openings are traversable/visible",true),new("Textures and normals render correctly",true),new("Placed props render and align",true),new("No blocking console errors",true)]; }
