using System.Numerics;
namespace ToneCustoms.ShellStudio.Core;
public enum SceneObjectType { Wall, Room, Floor, Ceiling, Door, Window, Stairs, Prop, Light }
public sealed class SceneObject { public Guid Id {get;set;}=Guid.NewGuid(); public string Name {get;set;}="Object"; public SceneObjectType Type {get;set;} public Vector3 Position {get;set;} public Vector3 Rotation {get;set;} public Vector3 Scale {get;set;}=Vector3.One; public string? MaterialId {get;set;} public bool Collision {get;set;}=true; public int FloorLevel {get;set;} }
public sealed class ShellProject { public string Name {get;set;}="Untitled"; public int Version {get;set;}=1; public List<SceneObject> Objects {get;set;}=[]; public List<MaterialAsset> Materials {get;set;}=[]; public ProjectSettings Settings {get;set;}=new(); }
public sealed class MaterialAsset { public string Id {get;set;}=Guid.NewGuid().ToString("N"); public string Name {get;set;}="Material"; public string? DiffuseDds {get;set;} public string? NormalDds {get;set;} public string? SpecularDds {get;set;} public float Tiling {get;set;}=1f; }
public sealed class ProjectSettings { public float GridSize {get;set;}=.25f; public bool SnapEnabled {get;set;}=true; public bool AdvancedMode {get;set;} public float PlayerHeight {get;set;}=1.8f; }
