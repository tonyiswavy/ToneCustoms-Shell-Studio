namespace ToneCustoms.ShellStudio.Textures;
public sealed record DdsInfo(bool Valid,uint Height,uint Width,uint MipMaps,string FourCC,long Bytes);
public sealed class DdsService {
 public DdsInfo Inspect(string path){using var f=File.OpenRead(path);using var b=new BinaryReader(f);if(new string(b.ReadChars(4))!="DDS ")return new(false,0,0,0,"",f.Length);var size=b.ReadUInt32();var flags=b.ReadUInt32();var h=b.ReadUInt32();var w=b.ReadUInt32();b.ReadUInt32();b.ReadUInt32();var mip=b.ReadUInt32();f.Position=84;var four=new string(b.ReadChars(4));return new(size==124,h,w,mip,four,f.Length);}
 public IEnumerable<string> Validate(DdsInfo d){if(!d.Valid)yield return "Invalid DDS header";if(d.Width>4096||d.Height>4096)yield return "Texture exceeds recommended 4096 size";if(d.MipMaps==0)yield return "DDS has no mipmaps";}
}
