using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace LiveKit
{
    public class LocalParticipant : Participant
    {
        [Preserve]
        internal LocalParticipant(JSHandle ptr) : base(ptr)
        {

        }

        public JSError LastCameraError()
        {
            var ptr = JSNative.CallMethod(NativePtr, "lastCameraError");
            if (JSNative.IsObject(ptr))
                return null;

            return Acquire<JSError>(ptr);
        }

        public JSError LastMicrophoneError()
        {
            var ptr = JSNative.CallMethod(NativePtr, "lastMicrophoneError");
            if (JSNative.IsObject(ptr))
                return null;

            return Acquire<JSError>(ptr);
        }

        public new LocalTrackPublication GetTrack(TrackSource source)
        {
            return base.GetTrack(source) as LocalTrackPublication;
        }

        public new LocalTrackPublication GetTrackByName(string name)
        {
            return base.GetTrackByName(name) as LocalTrackPublication;
        }

        public JSPromise<LocalTrackPublication> SetCameraEnabled(bool enabled)
        {
            JSNative.PushBoolean(enabled);
            return Acquire<JSPromise<LocalTrackPublication>>(JSNative.CallMethod(NativePtr, "setCameraEnabled"));
        }

        public JSPromise<LocalTrackPublication> SetMicrophoneEnabled(bool enabled)
        {
            JSNative.PushBoolean(enabled);
            return Acquire<JSPromise<LocalTrackPublication>>(JSNative.CallMethod(NativePtr, "setMicrophoneEnabled"));
        }

        public JSPromise<LocalTrackPublication> SetScreenShareEnabled(bool enabled)
        {
            JSNative.PushBoolean(enabled);
            return Acquire<JSPromise<LocalTrackPublication>>(JSNative.CallMethod(NativePtr, "setScreenShareEnabled"));
        }

        public JSPromise EnableCameraAndMicrophone()
        {
            return Acquire<JSPromise>(JSNative.CallMethod(NativePtr, "enableCameraAndMicrophone"));
        }

        public JSPromise<JSArray<LocalTrack>> CreateTracks(CreateLocalTracksOptions? options = null)
        {
            if (options != null)
                JSNative.PushStruct(JsonConvert.SerializeObject(options, JSNative.JsonSettings));

            return Acquire<JSPromise<JSArray<LocalTrack>>>(JSNative.CallMethod(NativePtr, "createTracks"));
        }

        public JSPromise<JSArray<LocalTrack>> CreateScreenTracks(ScreenShareCaptureOptions? options = null)
        {
            if (options != null)
                JSNative.PushStruct(JsonConvert.SerializeObject(options, JSNative.JsonSettings));

            return Acquire<JSPromise<JSArray<LocalTrack>>>(JSNative.CallMethod(NativePtr, "createScreenTracks"));
        }

        public JSPromise<LocalTrackPublication> PublishTrack(LocalTrack track, TrackPublishOptions? options = null)
        {
            JSNative.PushObject(track.NativePtr);
            if (options != null)
                JSNative.PushStruct(JsonConvert.SerializeObject(options, JSNative.JsonSettings));


            return Acquire<JSPromise<LocalTrackPublication>>(JSNative.CallMethod(NativePtr, "publishTrack"));
        }

        public JSPromise<LocalTrackPublication> PublishTrack(MediaStreamTrack track, TrackPublishOptions? options = null)
        {
            JSNative.PushObject(track.NativePtr);
            if (options != null)
                JSNative.PushStruct(JsonConvert.SerializeObject(options, JSNative.JsonSettings));

            return Acquire<JSPromise<LocalTrackPublication>>(JSNative.CallMethod(NativePtr, "publishTrack"));
        }

        public LocalTrackPublication UnpublishTrack(LocalTrack track, bool? stopOnUnpublish = null)
        {
            JSNative.PushObject(track.NativePtr);

            if(stopOnUnpublish != null)
                JSNative.PushBoolean(stopOnUnpublish.Value);

            var ptr = JSNative.CallMethod(NativePtr, "unpublishTrack");
            if (JSNative.IsObject(ptr))
                return null;
            
            return Acquire<LocalTrackPublication>(ptr);
        }

        public LocalTrackPublication UnpublishTrack(MediaStreamTrack track, bool? stopOnUnpublish = null)
        {
            JSNative.PushObject(track.NativePtr);

            if (stopOnUnpublish != null)
                JSNative.PushBoolean(stopOnUnpublish.Value);

            var ptr = JSNative.CallMethod(NativePtr, "unpublishTrack");
            if (JSNative.IsObject(ptr))
                return null;
            
            return Acquire<LocalTrackPublication>(ptr);
        }

        // TODO Support unsafe ptr
        public JSPromise PublishData(byte[] data, DataPacketKind kind, params RemoteParticipant[] participants)
        {
            return PublishData(data, 0, data.Length, kind, participants);
        }
        
        public JSPromise PublishData(byte[] data, int offset, int size, DataPacketKind kind, params RemoteParticipant[] participants)
        {
            JSArray<RemoteParticipant> arr = null;
            if(participants != null)
                arr = new JSArray<RemoteParticipant>(participants);

            JSNative.PushData(data, offset, size);
            JSNative.PushNumber((double)kind);

            if (participants == null)
                JSNative.PushUndefined();
            else
                JSNative.PushObject(arr.NativePtr);

            return Acquire<JSPromise>(JSNative.CallMethod(NativePtr, "publishData"));
        }
        
        public void SetTrackSubscriptionPermissions(bool allParticipantsAllowed, ParticipantTrackPermission[] participantTrackPermissions)
        {
            JSNative.PushBoolean(allParticipantsAllowed);
            JSNative.PushObject(new JSArray<ParticipantTrackPermission>(participantTrackPermissions).NativePtr);

            JSNative.CallMethod(NativePtr, "setTrackSubscriptionPermissions");
        }
    }
}