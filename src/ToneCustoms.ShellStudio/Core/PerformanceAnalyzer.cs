namespace ToneCustoms.ShellStudio.Core;
public sealed record PerformanceReport(int Objects,int CollisionObjects,int Materials,string Rating);
public sealed class PerformanceAnalyzer { public PerformanceReport Analyze(ShellProject p){var c=p.Objects.Count(x=>x.Collision);var rating=p.Objects.Count<250?"Good":p.Objects.Count<750?"Moderate":"Heavy";return new(p.Objects.Count,c,p.Materials.Count,rating);} }
