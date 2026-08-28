using System.Numerics;
namespace ToneCustoms.ShellStudio.Editor;
public enum CameraMode { Orbit, Fly, Walk }
public sealed class ViewportState { public CameraMode CameraMode {get;set;}=CameraMode.Orbit; public Vector3 CameraPosition {get;set;}=new(8,-8,6); public Vector3 CameraTarget {get;set;}=new(0,0,1.5f); public bool ShowGrid {get;set;}=true; public bool ShowCollision {get;set;} public bool ShowBounds {get;set;} public bool ShowMeasurements {get;set;}=true; public bool LightingPreview {get;set;}=true; public int ActiveFloor {get;set;} }
