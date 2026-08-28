using System.Security.Cryptography;
namespace ToneCustoms.ShellStudio.Update;
public sealed class IntegrityVerifier { public string Sha256(string path){using var s=File.OpenRead(path);return Convert.ToHexString(SHA256.HashData(s)).ToLowerInvariant();}public bool Matches(string path,string expected)=>Sha256(path).Equals(expected,StringComparison.OrdinalIgnoreCase); }
