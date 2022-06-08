using MagazineStructure;
using StructureComponents;
using UnityEngine;
using Utilities;

namespace WeaponStructure
{
    [RequireComponent(typeof(Collider))]
    public class WeaponLoader : MonoBehaviour
    {
        private Container<Magazine, MagazineType> _container;
        private Attachment _attachment;
        private Magazine _magazine;
        private WeaponAudio _weaponAudio;

        public void Init(Container<Magazine, MagazineType> container, Attachment attachment, WeaponAudio weaponAudio)
        {
            _weaponAudio = weaponAudio;
            _container = container;
            _attachment = attachment;
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (_magazine != null) return;
            if (!other.TryGetComponent<ParentReference<Magazine>>(out var reference)) return;
            var magazine = reference.GetReference();
            if (!magazine.GetMagazineAttachment().TryGetHand(out _)) return;
            // Using Vector3.Dot to provide information about magazine & weapon rotation

            var magazineUp = magazine.transform.up;
            var weaponUp = transform.up;

            var dot = Vector3.Dot(weaponUp, magazineUp);

            if (dot < 0.75f) return;

            _magazine = magazine;

            var magazineAttachment = _magazine.GetMagazineAttachment();
            magazineAttachment.OnDrop += OnMagazineDrop;
        }

        private void OnMagazineDrop()
        {
            if (!_attachment.TryGetHand(out _)) return;

            var pistolMagazineTypeName = _container.GetStoredType().MagazineName;
            var magazineTypeName = _magazine.GetMagazineType().MagazineName;
            if (!pistolMagazineTypeName.Equals(magazineTypeName)) return;

            if (!_container.TryPush(_magazine)) return;
            
            _weaponAudio.PlayLoadMagazineSound();

            var magazineAttachment = _magazine.GetMagazineAttachment();
            magazineAttachment.OnDrop -= OnMagazineDrop;
            _magazine.Deactivate();
            _magazine = null;
        }

        private void OnTriggerExit(Collider other)
        {
            if (_magazine == null) return;
            if (!other.transform.root.TryGetComponent<Magazine>(out var magazine)) return;
            if (_magazine != magazine) return;

            var magazineAttachment = _magazine.GetMagazineAttachment();
            magazineAttachment.OnDrop -= OnMagazineDrop;
            _magazine = null;
        }
    }
}