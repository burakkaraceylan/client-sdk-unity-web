using Newtonsoft.Json;
using System;
using UnityEngine.Scripting;

namespace LiveKit
{
    public class LocalVideoTrack : LocalTrack
    {

        public bool IsSimulcast
        {
            get
            {
                JSNative.PushString("isSimulcast");
                return JSNative.GetBoolean(JSNative.GetProperty(NativePtr));
            }
        }

        [Preserve]
        internal LocalVideoTrack(JSHandle ptr) : base(ptr)
        {
            
        }

        public void SetPublishingQuality(VideoQuality maxQuality)
        {
            JSNative.PushNumber((double)maxQuality);
            JSNative.CallMethod(NativePtr, "setPublishingQuality");
        }

        public JSPromise SetDeviceId(string deviceId)
        {
            JSNative.PushString(deviceId);
            return Acquire<JSPromise>(JSNative.CallMethod(NativePtr, "setDeviceId"));
        }

        public JSPromise RestartTrack(VideoCaptureOptions? options = null)
        {
            if (options != null)
                JSNative.PushStruct(JsonConvert.SerializeObject(options, JSNative.JsonSettings));

            return Acquire<JSPromise>(JSNative.CallMethod(NativePtr, "restartTrack"));
        }
    }
}