namespace ToneCustoms.ShellStudio.Core;
public sealed record SceneStatistics(int Objects,int Walls,int Floors,int Openings,int Props,int Materials,int CollisionObjects);
public sealed class SceneStatisticsService { public SceneStatistics Get(ShellProject p)=>new(p.Objects.Count,p.Objects.Count(x=>x.Type==SceneObjectType.Wall),p.Objects.Count(x=>x.Type==SceneObjectType.Floor),p.WallOpenings.Count,p.Objects.Count(x=>x.Type==SceneObjectType.Prop),p.Materials.Count,p.Objects.Count(x=>x.Collision)); }
