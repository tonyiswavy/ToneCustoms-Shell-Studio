# ToneCustoms Shell Studio

Standalone Windows FiveM shell creation studio designed to make shell building visual and simple while using Blender, Sollumz, and CodeWalker as the GTA/FiveM toolchain.

## Target workflow

Create → Build → Texture → Props → Validate → Export to FiveM

## Core scope

- Standalone Windows desktop application
- Real-time 3D shell viewport
- Walls, rooms, floors, ceilings, doors, windows, and stairs
- XYZ move / rotate / scale controls
- Save/load projects, undo/redo, autosave/recovery
- Material and texture system with first-class DDS support
- GTA and custom prop placement
- Automatic collision/bounds preparation
- Blender bridge
- Sollumz bridge
- CodeWalker integration/inspection workflow
- GTA/FiveM validation and actionable errors
- Automatic FiveM resource builder
- GitHub Actions Windows builds
- Update-ready architecture

## Completion standard

The project is not considered complete until the intended end-to-end workflow is tested: build a shell in Shell Studio, texture it, place supported assets, validate it, export it, install the generated resource on a FiveM test server, and verify visuals/collision in game with no known blocking issues.
