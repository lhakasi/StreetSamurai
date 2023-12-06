using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private VideoPlayer _videoPlayer;

    void Awake()
    {
        _rawImage.texture = _videoPlayer.texture;
        _videoPlayer.Play();
    }
}