#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.2.1
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class AkReflectionPathInfo : global::System.IDisposable {
  private global::System.IntPtr swigCPtr;
  protected bool swigCMemOwn;

  internal AkReflectionPathInfo(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  internal static global::System.IntPtr getCPtr(AkReflectionPathInfo obj) {
    return (obj == null) ? global::System.IntPtr.Zero : obj.swigCPtr;
  }

  internal virtual void setCPtr(global::System.IntPtr cPtr) {
    Dispose();
    swigCPtr = cPtr;
  }

  ~AkReflectionPathInfo() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkUnitySoundEnginePINVOKE.CSharp_delete_AkReflectionPathInfo(swigCPtr);
        }
        swigCPtr = global::System.IntPtr.Zero;
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public AkVector64 imageSource { set { AkUnitySoundEnginePINVOKE.CSharp_AkReflectionPathInfo_imageSource_set(swigCPtr, value); }  get { return AkUnitySoundEnginePINVOKE.CSharp_AkReflectionPathInfo_imageSource_get(swigCPtr); } 
  }

  public uint numPathPoints { set { AkUnitySoundEnginePINVOKE.CSharp_AkReflectionPathInfo_numPathPoints_set(swigCPtr, value); }  get { return AkUnitySoundEnginePINVOKE.CSharp_AkReflectionPathInfo_numPathPoints_get(swigCPtr); } 
  }

  public uint numReflections { set { AkUnitySoundEnginePINVOKE.CSharp_AkReflectionPathInfo_numReflections_set(swigCPtr, value); }  get { return AkUnitySoundEnginePINVOKE.CSharp_AkReflectionPathInfo_numReflections_get(swigCPtr); } 
  }

  public float level { set { AkUnitySoundEnginePINVOKE.CSharp_AkReflectionPathInfo_level_set(swigCPtr, value); }  get { return AkUnitySoundEnginePINVOKE.CSharp_AkReflectionPathInfo_level_get(swigCPtr); } 
  }

  public bool isOccluded { set { AkUnitySoundEnginePINVOKE.CSharp_AkReflectionPathInfo_isOccluded_set(swigCPtr, value); }  get { return AkUnitySoundEnginePINVOKE.CSharp_AkReflectionPathInfo_isOccluded_get(swigCPtr); } 
  }

  public static int GetSizeOf() { return AkUnitySoundEnginePINVOKE.CSharp_AkReflectionPathInfo_GetSizeOf(); }

  public UnityEngine.Vector3 GetPathPoint(uint idx) { return AkUnitySoundEnginePINVOKE.CSharp_AkReflectionPathInfo_GetPathPoint(swigCPtr, idx); }

  public uint GetTextureIDs(uint idx) { return AkUnitySoundEnginePINVOKE.CSharp_AkReflectionPathInfo_GetTextureIDs(swigCPtr, idx); }

  public float GetDiffraction(uint idx) { return AkUnitySoundEnginePINVOKE.CSharp_AkReflectionPathInfo_GetDiffraction(swigCPtr, idx); }

  public void Clone(AkReflectionPathInfo other) { AkUnitySoundEnginePINVOKE.CSharp_AkReflectionPathInfo_Clone(swigCPtr, AkReflectionPathInfo.getCPtr(other)); }

  public AkReflectionPathInfo() : this(AkUnitySoundEnginePINVOKE.CSharp_new_AkReflectionPathInfo(), true) {
  }

}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.