using MagazineStructure;
using StructureComponents;
using UnityEngine;
using Utilities;

namespace MagazinesRealization
{
    public class MagazineGlock9X19 : Magazine
    {
        public virtual void Init()
        {
            var references = GetComponentsInChildren<ParentReference<Magazine>>();
            foreach (var reference in references)
            {
                reference.Init(this);
            }

            MagazineAudio = GetComponent<MagazineAudio>();
            MagazineCartridgeContainer = GetComponent<MagazineCartridgeContainer>();
            MagazineCartridgeExtractor = GetComponent<MagazineCartridgeExtractor>();
            MagazinePopper = GetComponent<Popper>();
            MagazineAttachment = GetComponent<Attachment>();
            Rigidbody = GetComponent<Rigidbody>();

            MagazineCartridgeContainer.Init();
            MagazineAttachment.Init();
            MagazinePopper.Init(MagazineAttachment);
            MagazineCartridgeExtractor.Init(MagazineCartridgeContainer, MagazinePopper, MagazineAttachment,
                MagazineAudio);
            MagazineCartridgeInserter.Init(MagazineCartridgeContainer, MagazineAttachment, MagazineAudio);
            MagazineView.Init(MagazineCartridgeContainer);
        }

        private void Start()
        {
            Init();
        }
    }
}