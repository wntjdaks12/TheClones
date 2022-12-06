public class SkillStrategyFactory
{
    public  SkillStrategy ReturnSkillStrategy(uint id)
    {
        switch (id)
        {
            case 30001: return new EraseMemoryStrategy();
            case 30002: return new ForceSpawnStrategy();
            default: return null;
        }
    }
}
