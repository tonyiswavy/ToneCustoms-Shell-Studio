# Architecture
Shell Studio is a standalone Windows editor. The app owns project data, visual editing, materials, prop placement, validation and resource assembly. Blender runs headless for mesh processing. Sollumz performs GTA-specific conversion/export. CodeWalker is used for GTA asset inspection/validation. The exporter must never report success when a required GTA conversion step failed.

Major modules: Editor, Core geometry, Materials/DDS, Props, Collision/Bounds, Validation, Blender bridge, Sollumz bridge, CodeWalker bridge, FiveM export, autosave/versioning, dependency diagnostics and updater/build infrastructure.
