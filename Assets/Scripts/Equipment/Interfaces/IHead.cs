namespace Equipment.Interfaces
{
    public interface IHead : IEquippable
    {
        public float Suppression { get; }
        public string GetHeadEquipmentName();
    }
}