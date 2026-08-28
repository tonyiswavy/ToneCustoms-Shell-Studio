using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Materials;
public sealed class MaterialAssignmentService { public void Assign(ShellProject p,SceneObject o,string materialId){if(!p.Materials.Any(x=>x.Id==materialId))throw new KeyNotFoundException("Material not found");o.MaterialId=materialId;}public IEnumerable<SceneObject> Users(ShellProject p,string materialId)=>p.Objects.Where(x=>x.MaterialId==materialId); }
