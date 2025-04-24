namespace Conundrum.Model
{
    public class EnigmaMachineSetting : CipherSettingBase
    {

        // Simplified collection initialization (IDE0028)
        public List<RotorSetting> Rotors { get; set; } = new();

        // Simplified 'new' expression (IDE0090)
        public ReflectorSetting Reflector { get; set; } = new();

        // Simplified 'new' expression (IDE0090)
        public PlugBoardSetting Plugboard { get; set; } = new();

    }
}
