namespace ToneCustoms.ShellStudio.Bridges;
public sealed record CodeWalkerValidationResult(bool Launched,string Message);
public sealed class CodeWalkerValidationService { public CodeWalkerValidationResult Inspect(string? exe,string output){if(string.IsNullOrWhiteSpace(exe)||!File.Exists(exe))return new(false,"CodeWalker executable is not configured");if(!Directory.Exists(output))return new(false,"GTA output folder is missing");new CodeWalkerBridge().Open(exe,output);return new(true,"Opened verified GTA output for CodeWalker inspection");} }
