namespace Equipment.Interfaces
{
    public interface IHead : IEquippable
    { 
        public string GetHeadEquipmentName();
        public float GetHelmetSuppression();
    }
}