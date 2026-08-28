using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Materials;
public sealed class MaterialLibraryService { public IEnumerable<MaterialAsset> Search(ShellProject p,string query)=>p.Materials.Where(x=>x.Name.Contains(query,StringComparison.OrdinalIgnoreCase));public void Remove(ShellProject p,string id){if(p.Objects.Any(x=>x.MaterialId==id))throw new InvalidOperationException("Material is still assigned to scene objects");p.Materials.RemoveAll(x=>x.Id==id);} }
