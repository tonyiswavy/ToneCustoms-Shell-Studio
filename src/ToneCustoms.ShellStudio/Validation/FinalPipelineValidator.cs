using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Validation;
public sealed record PipelineCheck(string Name,bool Passed,string Detail);
public sealed class FinalPipelineValidator
{
 public IReadOnlyList<PipelineCheck> Check(ShellProject project,IEnumerable<string> gtaFiles){var files=gtaFiles.Where(File.Exists).ToList();var ext=files.Select(x=>Path.GetExtension(x).ToLowerInvariant()).ToHashSet();return new[]{new PipelineCheck("Scene",project.Objects.Count>0,project.Objects.Count>0?$"{project.Objects.Count} objects":"Project is empty"),new PipelineCheck("Drawable",ext.Contains(".ydr")||ext.Contains(".ymap"),"Requires verified GTA drawable/map output"),new PipelineCheck("Collision",ext.Contains(".ybn")||project.Objects.All(x=>!x.Collision),"Requires YBN when collision objects exist"),new PipelineCheck("Textures",project.Materials.Count==0||ext.Contains(".ytd")||project.Materials.All(x=>string.IsNullOrWhiteSpace(x.DiffuseDds)),"Requires YTD when DDS materials are used"),new PipelineCheck("Resource names",project.Name.All(c=>char.IsLetterOrDigit(c)||c is '_' or '-' or ' '),"Project/resource name must be filesystem safe")};}
 public bool Ready(ShellProject project,IEnumerable<string> gtaFiles)=>Check(project,gtaFiles).All(x=>x.Passed);
}
