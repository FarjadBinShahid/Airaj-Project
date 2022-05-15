#if DEBUG
#define VIDEOPLAYER_DEBUG
#endif

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Unity.VideoHelper
{

    public class VideoController : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private RawImage screen;

        [SerializeField]
        private bool startAfterPreparation = true;

        [Header("Optional")]

        [SerializeField]
        private VideoPlayer videoPlayer;

        [SerializeField]
        private AudioSource audioSource;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onPrepared = new UnityEvent();

        [SerializeField]
        private UnityEvent onStartedPlaying = new UnityEvent();

        [SerializeField]
        private UnityEvent onFinishedPlaying = new UnityEvent();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether to automatically start playing the video after it is prepared.
        /// </summary>
        public bool StartAfterPreparation
        {
            get { return startAfterPreparation; }
            set { startAfterPreparation = value; }
        }

        /// <summary>
        /// Gets a value between 0 and 1 that represents the video position.
        /// </summary>
        public float NormalizedTime
        {
            get { return (float)(VideoPlayer.time / Duration); }
        }

        /// <summary>
        /// Gets the duration of the video in seconds.
        /// </summary>
        public ulong Duration
        {
            get { return VideoPlayer.frameCount / (ulong)VideoPlayer.frameRate; }
        }

        /// <summary>
        /// Gets the current time in seconds.
        /// </summary>
        public ulong Time
        {
            get { return (ulong)VideoPlayer.time; }
        }

        /// <summary>
        /// Gets whether the player prepared buffer for smooth playback.
        /// </summary>
        public bool IsPrepared
        {
            get { return VideoPlayer.isPrepared; }
        }

        /// <summary>
        /// Gets whether the video is playing.
        /// </summary>
        public bool IsPlaying
        {
            get { return VideoPlayer.isPlaying; }
        }

        /// <summary>
        /// Gets or sets the volume of the audio source.
        /// </summary>
        public float Volume
        {
            get { return audioSource == null ? VideoPlayer.GetDirectAudioVolume(0) : audioSource.volume; }
            set
            {
                if (audioSource == null)
                    VideoPlayer.SetDirectAudioVolume(0, value);
                else
                    audioSource.volume = value;
            }
        }

        /// <summary>
        /// Gets or sets the image to show the video.
        /// </summary>
        public RawImage Screen
        {
            get { return screen; }
            set { screen = value; }
        }


        /// <summary>
        /// Fired when the video is prepared for playback.
        /// </summary>
        public UnityEvent OnPrepared
        {
            get { return onPrepared; }
        }

        /// <summary>
        /// Fired when the player started to play.
        /// </summary>
        public UnityEvent OnStartedPlaying
        {
            get { return onStartedPlaying; }
        }

        /// <summary>
        /// Fired when the video is finished.
        /// </summary>
        public UnityEvent OnFinishedPlaying
        {
            get { return onFinishedPlaying; }
        }

        public VideoPlayer VideoPlayer { get => videoPlayer; set => videoPlayer = value; }

        #endregion

        #region Unity methods

        private void Awake()
        {
            if (VideoPlayer == null)
            {
                VideoPlayer = gameObject.GetOrAddComponent<VideoPlayer>();
                SubscribeToVideoPlayerEvents();
            }
        }

        private void OnEnable()
        {
            SubscribeToVideoPlayerEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromVideoPlayerEvents();
        }

        private void Start()
        {
            if (VideoPlayer == null)
            {
                VideoPlayer = gameObject.GetOrAddComponent<VideoPlayer>();
                SubscribeToVideoPlayerEvents();
            }
            VideoPlayer.playOnAwake = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares the video player for the URL.
        /// </summary>
        /// <param name="url">The video URL.</param>
        public void PrepareForUrl(string url)
        {
            VideoPlayer.source = VideoSource.Url;
            VideoPlayer.url = url;
            VideoPlayer.Prepare();
        }

        /// <summary>
        /// Prepares the player for a video clip.
        /// </summary>
        /// <param name="clip">The clip.</param>
        public void PrepareForClip(VideoClip clip)
        {
            VideoPlayer.source = VideoSource.VideoClip;
            VideoPlayer.clip = clip;
            VideoPlayer.Prepare();
        }

        /// <summary>
        /// Plays a prepared video.
        /// </summary>
        public void Play()
        {
            if (!IsPrepared)
            {
                VideoPlayer.Prepare();
                return;
            }

            VideoPlayer.Play();
        }

        /// <summary>
        /// Pauses the player.
        /// </summary>
        public void Pause()
        {
            VideoPlayer.Pause();
        }

        /// <summary>
        /// Plays or pauses the video.
        /// </summary>
        public void TogglePlayPause()
        {
            if (IsPlaying)
                Pause();
            else
                Play();
        }

        /// <summary>
        /// Sets the playback speed.
        /// </summary>
        /// <param name="speed">The speed, e.g. 0.5 for half the normal speed.</param>
        public void SetPlaybackSpeed(float speed)
        {
            VideoPlayer.playbackSpeed = speed;
        }

        /// <summary>
        /// Jumps to the specified time in the video.
        /// </summary>
        /// <param name="time">The normalized time.</param>
        public void Seek(float time)
        {
            time = Mathf.Clamp(time, 0, 1);
            VideoPlayer.time = time * Duration;
        }

        #endregion

        #region Private Methods

        private void OnStarted(VideoPlayer source)
        {
            onStartedPlaying.Invoke();
        }

        private void OnFinished(VideoPlayer source)
        {
            onFinishedPlaying.Invoke();
        }

        private void OnPrepareCompleted(VideoPlayer source)
        {
            onPrepared.Invoke();
            screen.texture = VideoPlayer.texture;

#if VIDEOPLAYER_DEBUG
            Debug.LogWarning("[Video Controller] Depending on your Unity version you might not be able to hear audio in the Editor.");
#endif

            SetupAudio();
            SetupScreenAspectRatio();

            if (StartAfterPreparation)
                Play();
        }

        private void SetupScreenAspectRatio()
        {
            var fitter = screen.gameObject.GetOrAddComponent<AspectRatioFitter>();
            fitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
            fitter.aspectRatio = (float)VideoPlayer.texture.width / VideoPlayer.texture.height;
        }

        private void SetupAudio()
        {
            if (VideoPlayer.audioTrackCount <= 0)
                return;

            if(audioSource == null && VideoPlayer.canSetDirectAudioVolume)
            {
                VideoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
            }
            else
            {
                VideoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
                VideoPlayer.SetTargetAudioSource(0, audioSource);
            }
            VideoPlayer.controlledAudioTrackCount = 1;
            VideoPlayer.EnableAudioTrack(0, true);
        }

        private void OnError(VideoPlayer source, string message)
        {
#if VIDEOPLAYER_DEBUG
            Debug.LogError("[Video Controller] " + message);
#endif
        }

        private void SubscribeToVideoPlayerEvents()
        {
            if (VideoPlayer == null)
                return;

            VideoPlayer.errorReceived += OnError;
            VideoPlayer.prepareCompleted += OnPrepareCompleted;
            VideoPlayer.started += OnStarted;
            VideoPlayer.loopPointReached += OnFinished;
        }

        private void UnsubscribeFromVideoPlayerEvents()
        {
            if (VideoPlayer == null)
                return;

            VideoPlayer.errorReceived -= OnError;
            VideoPlayer.prepareCompleted -= OnPrepareCompleted;
            VideoPlayer.started -= OnStarted;
            VideoPlayer.loopPointReached -= OnFinished;
        }

        #endregion

    }

}
