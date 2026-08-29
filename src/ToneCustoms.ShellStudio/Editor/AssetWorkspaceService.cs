using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToneCustoms.ShellStudio.Core;
using ToneCustoms.ShellStudio.Props;
using ToneCustoms.ShellStudio.Import;

namespace ToneCustoms.ShellStudio.Editor;

public sealed class AssetWorkspaceService
{
    private readonly PropCatalog props = new();
    private readonly PropImportValidator imports = new();

    public IReadOnlyList<PropAsset> Props => props.Items;

    public MaterialAsset AddMaterial(ShellProject project, string texturePath)
    {
        if (!File.Exists(texturePath))
            throw new FileNotFoundException("Texture not found.", texturePath);

        var ext = Path.GetExtension(texturePath);
        var supported = new[] { ".dds", ".png", ".tga" };
        if (!supported.Contains(ext, StringComparer.OrdinalIgnoreCase))
            throw new InvalidDataException("Use DDS, PNG, or TGA textures.");

        var material = new MaterialAsset
        {
            Name = Path.GetFileNameWithoutExtension(texturePath),
            DiffuseDds = texturePath,
            Tiling = 1f
        };
        project.Materials.Add(material);
        return material;
    }

    public void AssignMaterial(SceneObject target, MaterialAsset material)
        => target.MaterialId = material.Id;

    public PropAsset ImportProp(string path) => props.AddCustom(path);

    public SceneObject CreateProp(PropAsset asset, Vec3 position, int floor) => new()
    {
        Name = asset.Name,
        Type = SceneObjectType.Prop,
        ModelName = asset.Model,
        SourcePath = asset.SourcePath,
        Position = position,
        Scale = Vec3.One,
        FloorLevel = floor,
        Collision = true
    };

    public IReadOnlyList<string> ValidateImport(string path)
    {
        var result = imports.Validate(path);
        return result.Valid ? Array.Empty<string>() : new[] { result.Message };
    }

    public IEnumerable<PropAsset> SearchProps(string? query, string? category = null)
        => props.Search(query, category);
}
