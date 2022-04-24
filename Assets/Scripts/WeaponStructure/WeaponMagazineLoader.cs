﻿using MagazineStructure;
using StructureComponents;
using UnityEngine;

namespace WeaponStructure
{
    [RequireComponent(typeof(Collider))]
    public class WeaponMagazineLoader : MonoBehaviour
    {
        private Container<Magazine, MagazineType> _container;
        private Attachment _attachment;
        private Magazine _magazine;

        public void Init(Container<Magazine, MagazineType> container, Attachment attachment)
        {
            _container = container;
            _attachment = attachment;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_magazine != null) return;
            if (other.transform.parent == null) return;
            if (!other.transform.parent.TryGetComponent<Magazine>(out var magazine)) return;
            if (!magazine.GetMagazineAttachment().TryGetHand(out var hand)) return;

            _magazine = magazine;

            var magazineAttachment = _magazine.GetMagazineAttachment();
            magazineAttachment.OnDrop += OnMagazineDrop;
        }

        private void OnMagazineDrop()
        {
            if (!_attachment.TryGetHand(out var hand)) return;

            var pistolMagazineTypeName = _container.GetStoredType().MagazineName;
            var magazineTypeName = _magazine.GetMagazineType().MagazineName;
            if (!pistolMagazineTypeName.Equals(magazineTypeName)) return;

            if (!_container.TryPush(_magazine)) return;

            var magazineAttachment = _magazine.GetMagazineAttachment();
            magazineAttachment.OnDrop -= OnMagazineDrop;
            _magazine.Deactivate();
            _magazine = null;
        }

        private void OnTriggerExit(Collider other)
        {
            if (_magazine == null) return;
            if (other.transform.parent == null) return;
            if (!other.transform.parent.TryGetComponent<Magazine>(out var magazine)) return;
            if (_magazine != magazine) return;

            var magazineAttachment = _magazine.GetMagazineAttachment();
            magazineAttachment.OnDrop -= OnMagazineDrop;
            _magazine = null;
        }
    }
}