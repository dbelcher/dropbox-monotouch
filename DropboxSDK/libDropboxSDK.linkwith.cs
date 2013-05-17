using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libDropboxSDK.a", LinkTarget.Simulator | LinkTarget.ArmV7, ForceLoad = true, Frameworks = "Security QuartzCore")]
