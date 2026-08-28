using System.Numerics;using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Preview;
public sealed class WalkPreview { public Vector3 Position {get;private set;}=new(0,0,1.8f); public float EyeHeight {get;set;}=1.65f; public void Spawn(Vector3 floorPoint)=>Position=floorPoint+new Vector3(0,0,EyeHeight); public void Move(Vector3 delta,ShellProject project)=>Position+=delta; }
