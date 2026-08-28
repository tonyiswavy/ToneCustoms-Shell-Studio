# End-to-end test plan
1. Launch clean Windows build.
2. Detect/configure Blender, Sollumz and CodeWalker.
3. Create project from empty and starter templates.
4. Build rooms using walls/floors/ceilings/openings/stairs across floors.
5. Exercise selection, snapping, transforms, duplication, measurements, save/reopen and recovery.
6. Apply DDS materials and verify mipmaps/format diagnostics.
7. Place GTA/custom props.
8. Generate/inspect collision and bounds.
9. Validate and run safe autofixes.
10. Run Blender/Sollumz conversion and inspect in CodeWalker.
11. Build FiveM resource and test it on a FiveM server.
12. Verify shell appearance, collision, openings, props and player traversal in game.
13. Reopen the project and reproduce the export.
14. Verify failed dependencies/export steps are reported as failures rather than false success.
