# Moiré

![Two curves repeated across the screen, forming a black on white Moiré pattern](Moir%C3%A9.png)

## Overview

A simple image formed using [`DuplicateSpline.cs`](https://github.com/richardfxr/unity-explorations/blob/01-Moire/Assets/DuplicateSpline.cs), which repeats [`SplineA`](https://github.com/richardfxr/unity-explorations/blob/01-Moire/Assets/Objects/SplineA.prefab) and [`SplineB`](https://github.com/richardfxr/unity-explorations/blob/01-Moire/Assets/Objects/SplineB.prefab) according to the following parameters:

- `splineContainer`: an empty game object that will become the parent for all duplicated splines.
- `numSplines`: the number of times each spline is duplicated
- `offsetY`: the amount of offset on the y-axis for the first spline
- `offsetYIncrement`: how much the y-axis offset will increase with each new spline
- `offsetA` & `offsetB`: the base offset in (x, y, z) for the corresponding spline

## References

- [AnanDEV’s video on Unity splines](https://youtu.be/gzPjH7-gHLE?si=kMe1OBcwttS7SJTJ)
- [Unity documentation on custom shaders](https://docs.unity3d.com/Manual/SL-VertexFragmentShaderExamples.html): specifically, the “Even simpler single color shader” section
- [whosdoom's comment](https://discussions.unity.com/t/how-to-duplicate-a-gameobject-using-scripting/76987/3) on using `GameObject.Instantiate` to duplicate `GameObject`s