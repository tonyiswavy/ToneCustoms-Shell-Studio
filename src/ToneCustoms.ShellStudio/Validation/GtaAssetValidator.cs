namespace ToneCustoms.ShellStudio.Validation;
public sealed record GtaAssetCheck(string File,bool Valid,string Detail);
public sealed class GtaAssetValidator
{
 static readonly HashSet<string> Known=new(StringComparer.OrdinalIgnoreCase){".ydr",".ydd",".ybn",".ytd",".ymap",".ytyp"};
 public IReadOnlyList<GtaAssetCheck> Validate(IEnumerable<string> files){var result=new List<GtaAssetCheck>();foreach(var f in files.Distinct(StringComparer.OrdinalIgnoreCase)){if(!File.Exists(f)){result.Add(new(f,false,"File missing"));continue;}var ext=Path.GetExtension(f);if(!Known.Contains(ext)){result.Add(new(f,false,"Unsupported GTA stream extension"));continue;}var size=new FileInfo(f).Length;result.Add(new(f,size>0,size>0?$"{ext} ready ({size:N0} bytes)":"File is empty"));}return result;}
 public IReadOnlyList<string> PipelineIssues(IEnumerable<string> files,bool needsCollision,bool needsTextures){var valid=Validate(files).Where(x=>x.Valid).Select(x=>Path.GetExtension(x.File).ToLowerInvariant()).ToHashSet();var issues=new List<string>();if(!valid.Overlaps(new[]{".ydr",".ymap"}))issues.Add("Missing drawable/map output (.ydr or .ymap).");if(needsCollision&&!valid.Contains(".ybn"))issues.Add("Missing collision output (.ybn).");if(needsTextures&&!valid.Contains(".ytd"))issues.Add("Missing texture dictionary (.ytd).");return issues;}
}
