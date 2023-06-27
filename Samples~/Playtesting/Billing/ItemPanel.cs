using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.VKGames.Samples.Playtesting
{
    public class ItemPanel : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _titleText;
        [SerializeField] private RawImage _iconImage;

        private Item _item;
        
        public event Action<ItemPanel> ButtonClicked;

        public Item Item => _item;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        public void Initialize(Item item)
        {
            if (_item != null)
                throw new InvalidOperationException();
            
            _item = item;
            _titleText.text = item.en_title;
            StartCoroutine(DownloadAndSetItemImage(item.photo_url));
        }

        private void OnButtonClick()
        {
            ButtonClicked?.Invoke(this);
        }
        
        private IEnumerator DownloadAndSetItemImage(string imageUrl)
        {
            var remoteImage = new RemoteImage(imageUrl);
            remoteImage.Download();

            while (!remoteImage.IsDownloadFinished)
                yield return null;

            if (remoteImage.IsDownloadSuccessful)
                _iconImage.texture = remoteImage.Texture;
        }
    }
}