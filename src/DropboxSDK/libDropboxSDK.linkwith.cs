using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libDropboxSDK.a", LinkTarget.Simulator | LinkTarget.ArmV7 | LinkTarget.ArmV7s, ForceLoad = true, Frameworks = "Security QuartzCore")]